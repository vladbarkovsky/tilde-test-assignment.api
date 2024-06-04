using Microsoft.EntityFrameworkCore;
using TildeTestAssignment.ORM.Entities;

namespace TildeTestAssignment.ORM.Services.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}