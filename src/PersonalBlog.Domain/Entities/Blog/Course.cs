using PersonalBlog.Domain.Commons.Base;

namespace PersonalBlog.Domain.Entities.Blog;

/// <summary>
/// سری آموزشی؛ یک پست می‌تواند بخشی از یک دوره باشد (Course.cs در اسپک).
/// </summary>
public class Course : EntityBase
{
    public string Title { get; set; } = default!;
    public string Slug { get; set; } = default!;
    public string? Description { get; set; }
    public string? CoverImageUrl { get; set; }
    public bool IsPublished { get; set; } = true;

    public ICollection<Post> Posts { get; set; } = new List<Post>();
}
