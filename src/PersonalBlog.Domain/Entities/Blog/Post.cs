using PersonalBlog.Domain.Commons.Base;

namespace PersonalBlog.Domain.Entities.Blog;

public class Post : EntityBase, ISoftDeletable
{
    public string Title { get; set; } = default!;
    public string Slug { get; set; } = default!;
    public string Summary { get; set; } = default!;
    public string Content { get; set; } = default!;   // HTML (پاکسازی‌شده با HtmlSanitizer)
    public string? CoverImageUrl { get; set; }

    public bool IsPublished { get; set; }
    public DateTime? PublishedAt { get; set; }

    public int ViewCount { get; set; }

    public long CategoryId { get; set; }
    public Category Category { get; set; } = default!;

    public long? CourseId { get; set; }
    public Course? Course { get; set; }
    public int? OrderInCourse { get; set; }

    // SEO
    public string? MetaTitle { get; set; }
    public string? MetaDescription { get; set; }
    public string? OgImageUrl { get; set; }

    public bool IsDeleted { get; set; }
    public long? DeletedBy { get; set; }
    public DateTime? DeletedAt { get; set; }

    public ICollection<PostTag> PostTags { get; set; } = new List<PostTag>();
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    public ICollection<PostVisit> Visits { get; set; } = new List<PostVisit>();
}
