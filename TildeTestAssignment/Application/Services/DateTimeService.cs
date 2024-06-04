using TildeTestAssignment.Application.Services.Interfaces;

namespace TildeTestAssignment.Application.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTimeOffset UtcNow
        {
            get
            {
                return DateTimeOffset.UtcNow;
            }
        }
    }
}