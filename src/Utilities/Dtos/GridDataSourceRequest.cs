using System.Text.Json.Serialization;

namespace PersonalBlog.Utilities.Dtos;

public class GridDataSourceRequest
{
    public GridDataSourceRequest()
    {
        InputParams = new();
    }

    public GridDataSourceRequest(int? page = null, int? pageSize = null) : this()
    {
        Page = (page.HasValue && page.Value > 0) ? page.Value : 1;
        PageSize = (pageSize.HasValue && pageSize.Value > 0) ? pageSize.Value : 10;
    }

    [JsonPropertyName(name: "page")]
    public int Page { get; set; } = 1;

    [JsonPropertyName(name: "pageSize")]
    public int PageSize { get; set; } = 10;

    [JsonPropertyName(name: "inputParams")]
    public GridParamsInputDto InputParams { get; set; } = new();

    [JsonIgnore]
    public int Skip => (Page - 1) * PageSize;

    [JsonIgnore]
    public int Take => PageSize;

    public void Sanitize(int minimumPageSize = 5)
    {
        this.Page = this.Page < 1 ? 1 : this.Page;
        this.PageSize = this.PageSize < minimumPageSize ? minimumPageSize : this.PageSize;
    }
}

