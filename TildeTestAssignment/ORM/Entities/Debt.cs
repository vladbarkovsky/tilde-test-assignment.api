namespace TildeTestAssignment.ORM.Entities
{
    public class Debt : BaseEntity
    {
        public Guid CreditorId { get; set; }
        public Person Creditor { get; set; }
        public Guid DebtorId { get; set; }
        public Person Debtor { get; set; }
        public decimal Amount { get; set; }
        public decimal Refunded { get; set; }
        public List<Refund> Refunds { get; set; }
    }
}