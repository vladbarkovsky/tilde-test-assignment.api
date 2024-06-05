using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Text.Json;
using TildeTestAssignment.Application.Services.Interfaces;
using TildeTestAssignment.ORM.Entities;
using TildeTestAssignment.ORM.Entities.Interfaces;
using TildeTestAssignment.ORM.Services.Interfaces;

namespace TildeTestAssignment.ORM.Services
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly IDateTimeService _dateTimeService;

        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Debt> Debts { get; set; }
        public DbSet<Refund> Refunds { get; set; }

        public ApplicationDbContext(DbContextOptions options, IDateTimeService dateTimeService) : base(options)
        {
            _dateTimeService = dateTimeService;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SyncRowVersion();
            AddAuditInfo();
            CreateAuditLogs();
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            ConfigureQueryFilters(modelBuilder);
        }

        // TODO: Test.
        private void SyncRowVersion()
        {
            ChangeTracker
                .Entries<IConsistent>()
                .AsParallel()
                .ForAll(entry =>
                {
                    var propertyEntry = entry.Property(x => x.RowVersion);

                    // Ensuring that the original value will came from the client - not from the database.
                    propertyEntry.OriginalValue = propertyEntry.CurrentValue;
                });
        }

        private void AddAuditInfo()
        {
            var utcNow = _dateTimeService.UtcNow;

            ChangeTracker
                .Entries<IAuditable>()
                .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified)
                .AsParallel()
                .ForAll(x =>
                {
                    var entity = x.Entity;

                    if (x.State == EntityState.Added)
                    {
                        entity.CreatedAt = utcNow;
                    }

                    if (x.State == EntityState.Modified)
                    {
                        entity.UpdatedAt = utcNow;
                    }
                });
        }

        // TODO: Try use parallelization.
        private void CreateAuditLogs()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(x =>
                    x.Entity is not AuditLog &&
                    x.State != EntityState.Detached &&
                    x.State != EntityState.Unchanged)

                /// Preventing <see cref="InvalidOperationException"/> with message 'Collection was modified; enumeration operation may not execute.'
                ;
            // .ToList();

            var utcNow = _dateTimeService.UtcNow;

            foreach (var entry in entries)
            {
                var auditLog = new AuditLog()
                {
                    EntityName = entry.Entity.GetType().Name,
                    CreatedAt = utcNow,
                };

                var primaryKeys = new Dictionary<string, string>();
                var oldValues = new Dictionary<string, object>();
                var newValues = new Dictionary<string, object>();
                var affectedProperties = new List<string>();

                foreach (var property in entry.Properties)
                {
                    var propertyName = property.Metadata.Name;

                    if (property.Metadata.IsPrimaryKey())
                    {
                        primaryKeys[propertyName] = property.CurrentValue.ToString();
                    }

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditLog.Operation = AuditLogOperation.Create;
                            newValues[propertyName] = property.CurrentValue;
                            break;

                        case EntityState.Modified:
                            if (property.IsModified)
                            {
                                auditLog.Operation = AuditLogOperation.Update;
                                oldValues[propertyName] = property.OriginalValue;
                                newValues.Add(propertyName, property.CurrentValue);
                                affectedProperties.Add(propertyName);
                            }

                            break;

                        case EntityState.Deleted:
                            auditLog.Operation = AuditLogOperation.Delete;
                            oldValues[propertyName] = property.OriginalValue;
                            break;
                    }
                }

                auditLog.EntityPrimaryKeys = primaryKeys.Any() ? JsonSerializer.Serialize(primaryKeys) : null;
                auditLog.OldValues = oldValues.Any() ? JsonSerializer.Serialize(oldValues) : null;
                auditLog.NewValues = newValues.Any() ? JsonSerializer.Serialize(newValues) : null;
                auditLog.AffectedProperties = affectedProperties.Any() ? JsonSerializer.Serialize(affectedProperties) : null;

                AuditLogs.Add(auditLog);
            }
        }

        public void ConfigureQueryFilterForDeletedEntities<TEntity>(ModelBuilder modelBuilder) where TEntity : class, IDeletable
        {
            modelBuilder.Entity<TEntity>().HasQueryFilter(x => !x.Deleted);
        }

        private void ConfigureQueryFilters(ModelBuilder modelBuilder)
        {
            var entityTypes = modelBuilder.Model.GetEntityTypes().Select(x => x.ClrType);
            var deletableEntityTypes = entityTypes.Where(x => typeof(IDeletable).IsAssignableFrom(x));
            var methodInfo = typeof(ApplicationDbContext).GetMethod(nameof(ConfigureQueryFilterForDeletedEntities));

            foreach (var entityType in deletableEntityTypes)
            {
                var method = methodInfo.MakeGenericMethod(entityType);
                method.Invoke(this, new object[] { modelBuilder });
            }
        }
    }
}