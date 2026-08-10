using PersonalBlog.Domain.Commons.Base;

namespace PersonalBlog.Domain.Entities;

public class CoursePost : EntityBase
{
    public int PostId { get; set; }

    public string CourseId { get; set; }
    public string? Title { get; set; } = default!;
    public string? Description { get; set; }
    public string? CoverImageUrl { get; set; }
    public bool IsPublished { get; set; } = true;
    public int OrderInCourse { get; set; }
    public virtual Course Course { get; set; }
    public virtual Post Post { get; set; }
}