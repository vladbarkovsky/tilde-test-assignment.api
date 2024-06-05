namespace TildeTestAssignment.Application.Statistics.Models
{
    public class BiggestDebtorCreditorVM
    {
        public Guid PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal TotalDebt { get; set; }
        public decimal TotalCredit { get; set; }
    }
}