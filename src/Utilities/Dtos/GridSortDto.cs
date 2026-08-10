using System.Text.Json.Serialization;

namespace PersonalBlog.Utilities.Dtos;

public class GridSortDto
{
    public GridSortDto()
    {
        Ascending = true;
    }

    [JsonPropertyName(name: "propertyName")]
    public string PropertyName { get; set; }

    [JsonPropertyName(name: "ascending")]
    public bool Ascending { get; set; }
}

