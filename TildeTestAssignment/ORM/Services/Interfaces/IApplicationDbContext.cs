using Microsoft.EntityFrameworkCore;
using TildeTestAssignment.ORM.Entities;

namespace TildeTestAssignment.ORM.Services.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Debt> Debts { get; set; }
        public DbSet<Refund> Refunds { get; set; }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}