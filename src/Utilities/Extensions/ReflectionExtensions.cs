namespace PersonalBlog.Utilities.Extensions;

public static class ReflectionExtensions
{
    public static bool HasProperty(this object obj, string propertyName)
    {
        return obj.GetType().GetProperty(propertyName) != null;
    }
}