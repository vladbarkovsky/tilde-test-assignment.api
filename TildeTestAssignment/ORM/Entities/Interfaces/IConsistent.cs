namespace TildeTestAssignment.ORM.Entities.Interfaces
{
    public interface IConsistent
    {
        public byte[] RowVersion { get; set; }
    }
}