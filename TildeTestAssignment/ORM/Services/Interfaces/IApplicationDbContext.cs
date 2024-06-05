using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using TildeTestAssignment.ORM.Entities;

namespace TildeTestAssignment.ORM.Services.Interfaces
{
    public interface IApplicationDbContext
    {
        DatabaseFacade Database { get; }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Debt> Debts { get; set; }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}