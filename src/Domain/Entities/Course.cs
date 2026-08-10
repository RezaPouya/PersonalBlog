using PersonalBlog.Domain.Commons.Base;

namespace PersonalBlog.Domain.Entities;

/// <summary>
/// سری آموزشی؛ یک پست می‌تواند بخشی از یک دوره باشد (Course.cs در اسپک).
/// </summary>
public class Course : EntityBase
{
    public Course()
    {
        this.TinyUrl = "c_" + Guid.CreateVersion7().ToString("N").Substring(0, 4).ToLower();
    }

    public string Title { get; set; } = default!;
    public string Slug { get; set; } = default!;
    public string TinyUrl { get; set; }
    public string? Description { get; set; }
    public string? CoverImageUrl { get; set; }
    public bool IsPublished { get; set; } = true;

    public bool IsInEnglish { get; set; }

    public int OrderInCourses { get; set; }

    public List<int> RelatedCourses { get; set; } = new List<int>();
    public ICollection<CoursePost> CoursePosts { get; set; } = new List<CoursePost>();
}
