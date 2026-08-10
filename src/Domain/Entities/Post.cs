using PersonalBlog.Domain.Commons.Base;
using PersonalBlog.Domain.Entities.Categories;

namespace PersonalBlog.Domain.Entities;

public class Post : EntityBase, ISoftDelete
{
    public Post()
    {
        this.TinyUrl = "p_" + Guid.CreateVersion7().ToString("N").Substring(0, 4).ToLower();
    }

    public string Title { get; set; } = default!;
    public string Slug { get; set; } = default!;
    public string TinyUrl { get; set; }
    public string Summary { get; set; } = default!;
    public string Content { get; set; } = default!;   // HTML (پاکسازی‌شده با HtmlSanitizer)
    public string? CoverImageUrl { get; set; }

    public bool IsPublished { get; set; }
    public DateTime? PublishedAt { get; set; }

    public int ViewCount { get; set; }

    public int CategoryId { get; set; }
    public Category Category { get; set; } = default!;


    // SEO
    public string? MetaTitle { get; set; }
    public string? MetaDescription { get; set; }
    public string? OgImageUrl { get; set; }

    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
    public bool IsCommentsEnabled { get; set; }
    public bool IsInEnglish { get; set; }

    public List<int> RelatedPosts { get; set; } = new List<int>();
    public ICollection<PostTag> PostTags { get; set; } = new List<PostTag>();
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    public ICollection<PostVisit> Visits { get; set; } = new List<PostVisit>();
    public ICollection<CoursePost> CoursePosts { get; set; } = new List<CoursePost>();
}

