using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TildeTestAssignment.ORM.Entities;
using TildeTestAssignment.ORM.Services.Interfaces;

namespace TildeTestAssignment.ORM.Services
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Debt> Debts { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}