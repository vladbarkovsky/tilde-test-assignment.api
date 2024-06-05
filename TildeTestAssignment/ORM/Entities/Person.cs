namespace TildeTestAssignment.ORM.Entities
{
    public class Person : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Balance { get; set; }
        public List<Transaction> DebtorTransactions { get; set; }
        public List<Transaction> CreditorTransactions { get; set; }
    }
}