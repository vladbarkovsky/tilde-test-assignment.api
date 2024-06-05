using TildeTestAssignment.Application.Common;

namespace TildeTestAssignment.ORM.Services.Interfaces
{
    public interface ISeedingService : IScopedService
    {
        public Task SeedAsync();
    }
}