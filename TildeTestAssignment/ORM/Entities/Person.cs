namespace TildeTestAssignment.ORM.Entities
{
    public class Person : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Balance { get; set; }
        public List<Debt> DebtorDebts { get; set; }
        public List<Debt> CreditorDebts { get; set; }
    }
}