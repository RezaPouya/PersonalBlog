using Newtonsoft.Json;

namespace PersonalBlog.Utilities.Extensions;

public static class GenericConventionExtensions
{
    public static List<TItem> ConvertToDeserializedList<TItem>(this string column)
    {
        return string.IsNullOrEmpty(column) ?
            new List<TItem>() :
            JsonConvert.DeserializeObject<List<TItem>>(column);
    }

    public static string ConvertToSerializedString<TItem>(this List<TItem> prop)
    {
        if (prop is null)
            return string.Empty;

        return prop.Any() ? JsonConvert.SerializeObject(prop) : "";
    }

    public static string ConvertToSerializedString<TItem>(this ICollection<TItem> prop)
    {
        if (prop is null)
            return string.Empty;

        return prop.Any() ? JsonConvert.SerializeObject(prop) : "";
    }
}