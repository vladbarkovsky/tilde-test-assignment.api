using TildeTestAssignment.ORM.Entities.Interfaces;

namespace TildeTestAssignment.ORM.Entities
{
    public class BaseEntity : IConsistent, IDeletable, IAuditable
    {
        public Guid Id { get; set; }
        public byte[] RowVersion { get; set; }
        public bool Deleted { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
    }
}