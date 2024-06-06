namespace TildeTestAssignment.Application.Common
{
    public interface ISortQuery
    {
        public string? SortBy { get; set; }
        public SortDirection? SortDirection { get; set; }
    }

    public enum SortDirection
    {
        Ascending = 0,
        Descending = 1
    }
}
