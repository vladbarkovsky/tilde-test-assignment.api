namespace TildeTestAssignment.Application.Statistics.Models
{
    public class TopDataSetVM
    {
        public int Count { get; set; }
        public decimal Sum { get; set; }
        public List<TopPersonVM> Persons { get; set; }
    }
}