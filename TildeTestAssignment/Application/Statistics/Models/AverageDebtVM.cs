namespace TildeTestAssignment.Application.Statistics.Models
{
    public class AverageDebtVM
    {
        public Guid PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Debt { get; set; }
    }
}