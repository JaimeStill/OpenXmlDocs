namespace DocBuilder.Core.ApiQuery
{
    public class QueryOptions
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; }
        public string? Search { get; set; }
        public string SortProperty { get; set; } = "Id";
        public bool SortDescending { get; set; }
    }
}