namespace TildeTestAssignment.ORM.Entities
{
    public class Person : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Balance { get; set; }
        public List<Transaction> Loans { get; set; }
        public List<Transaction> Refunds { get; set; }
    }
}