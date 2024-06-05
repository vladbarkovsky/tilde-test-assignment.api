namespace TildeTestAssignment.ORM.Entities
{
    public class Debt : BaseEntity
    {
        public Guid DebtorId { get; set; }
        public Person Debtor { get; set; }
        public Guid CreditorId { get; set; }
        public Person Creditor { get; set; }
        public decimal Amount { get; set; }
        public decimal Refunded { get; set; }
    }
}