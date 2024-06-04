namespace TildeTestAssignment.ORM.Entities.Interfaces
{
    public interface IAuditable
    {
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
    }
}