using TildeTestAssignment.Application.Common.Mapping.Interfaces;
using TildeTestAssignment.ORM.Entities;

namespace TildeTestAssignment.Application.Statistics.Models
{
    public class TopPersonVM : IMappedFrom<Person>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}