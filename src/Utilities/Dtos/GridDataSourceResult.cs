namespace PersonalBlog.Utilities.Dtos;

public class GridDataSourceResult<T> where T : class
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public int Totals { get; set; }
    public List<T> Data { get; set; }
}