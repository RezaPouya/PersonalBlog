namespace PersonalBlog.Utilities.Extensions;

public static class TypeExtensions
{
    public static bool ContainsProperty(this Type type, string propertyName)
    {
        return type.GetProperties().Where(p => p.Name.ToLower() == propertyName.ToLower()).Any();
    }
}