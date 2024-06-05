namespace TildeTestAssignment.Application.Statistics.Models
{
    public class BestDebtorVM
    {
        public Guid PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal RefundedRelative { get; set; }
        public decimal TotalDebtAmount { get; set; }
    }
}