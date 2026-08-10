using PersonalBlog.Utilities.Extensions;

namespace Abin.Website.Domain.Entities.SEOs;

public static class SlugTemplate
{
    public const string Post = "/{0}";
    public const string TinyUrl = "/{0}";
    public const string Category = "/category/{0}";
    public const string Project = "/project/{0}";
    //public const string Article = "/posts/{0}";
    public const string Course = "/course/{0}";
    public const string Author = "/author/{0}";

    public static string GetUrl(string breadcrumb, string template, string url)
    {
        string str = string.Format(template, url).StringNormalization(toLower: true);

        if (string.IsNullOrEmpty(str))
            str = string.Format(template, url).StringNormalization(toLower: true);

        str = str.ReplaceMultipleSpaceWithSingleOne();
        str = str.ReplaceMultipleSpaceWithSingleOne();
        str = str.ReplaceMultipleSpaceWithSingleOne();
        str = str.ReplaceMultipleSpaceWithSingleOne()
            .Replace("&", "")
            .Replace("=", "")
            .Replace("%", "")
            .Replace("#", "")
            .Replace("+", "")
            .Replace("*", "")
            .Replace(":", "")
            .Replace("?", "")
            .Replace(";", "")
            .Replace("@", "")
            .Replace(",", "")
            .Replace("[", "")
            .Replace("]", "")
            .Replace("{", "")
            .Replace("}", "")
            .Replace("|", "")
            .Replace("\\", "")
            .Replace("\"", "")
            .Replace("'", "");

        str = str.ReplaceMultipleSpaceWithSingleOne().Replace("--", "-").Replace(" ", "-");

        breadcrumb = breadcrumb?.Trim() ?? "";

        if (!string.IsNullOrEmpty(breadcrumb))
        {
            str = CombineUrlPaths(breadcrumb, str);
        }

        return str;
    }

    private static string CombineUrlPaths(string basePath, string relativePath)
    {
        relativePath = relativePath.Trim('/');

        if (string.IsNullOrEmpty(basePath))
            return relativePath;

        if (string.IsNullOrEmpty(relativePath))
            return basePath;

        var url = $"{basePath}/{relativePath}";

        url.Replace("//", "/");

        return url;
    }
}
