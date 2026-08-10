using System.Text.Json.Serialization;

namespace PersonalBlog.Utilities.Dtos;

public class GridParamsInputDto
{
    [JsonConstructor]
    public GridParamsInputDto()
    {
        Filters = new List<GridPropertyFilterDto>();
        //Sort = new GridSortDto();
    }

    [JsonPropertyName(name: "filters")]
    public List<GridPropertyFilterDto> Filters { get; set; }

    [JsonPropertyName(name: "sort")]
    public GridSortDto? Sort { get; set; }
}

