namespace PersonalBlog.Domain.Constants;

public static class CacheKeys
{
    public const string LatestPosts = "cache:posts:latest";
    public const string PopularPosts = "cache:posts:popular";
    public const string CategoriesList = "cache:categories:all";
    public const string TagsList = "cache:tags:all";
    public const string CoursesList = "cache:courses:all";
    public const string ProjectsList = "cache:projects:all";
    public const string SocialLinks = "cache:settings:social-links";

    public static string PostBySlug(string slug) => $"cache:post:{slug}";
    public static string PostsByCategory(long categoryId) => $"cache:posts:category:{categoryId}";
    public static string PostsByTag(long tagId) => $"cache:posts:tag:{tagId}";
}
