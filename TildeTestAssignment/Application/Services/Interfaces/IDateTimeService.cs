using TildeTestAssignment.Application.Common;

namespace TildeTestAssignment.Application.Services.Interfaces
{
    public interface IDateTimeService : IScopedService
    {
        public DateTimeOffset UtcNow { get; }
    }
}