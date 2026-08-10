namespace PersonalBlog.Utilities.Dtos;

public class GridDataSourceRequestAllowedParameters
{
    public GridDataSourceRequestAllowedParameters()
    {
        this.AllowedFilterProperties = new();
        this.AllowedSortProperties = new List<string>();
    }

    public List<GridAllowedPropertyFilterDto> AllowedFilterProperties { get; set; }
    public List<string> AllowedSortProperties { get; set; }
}