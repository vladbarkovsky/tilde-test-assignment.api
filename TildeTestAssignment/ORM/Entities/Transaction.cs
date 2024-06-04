namespace TildeTestAssignment.ORM.Entities
{
    public class Transaction : BaseEntity
    {
        public Guid CreditorId { get; set; }
        public Person Creditor { get; set; }
        public Guid DebtorId { get; set; }
        public Person Debtor { get; set; }
        public decimal Amount { get; set; }
    }
}