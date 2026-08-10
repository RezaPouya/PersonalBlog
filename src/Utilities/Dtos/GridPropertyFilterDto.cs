using System.Text.Json.Serialization;

namespace PersonalBlog.Utilities.Dtos;

public class GridPropertyFilterDto
{
    [JsonPropertyName(name: "propertyName")]
    public string PropertyName { get; set; }

    [JsonPropertyName(name: "operation")]
    public string Operation { get; set; }

    [JsonPropertyName(name: "value")]
    public string Value { get; set; }
}

