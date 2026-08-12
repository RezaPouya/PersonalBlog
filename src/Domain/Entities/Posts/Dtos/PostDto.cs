using DNTPersianUtils.Core;

namespace PersonalBlog.Domain.Entities.Posts.Dtos;

public class PostDto
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string Slug { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string TinyUrl { get; set; }
    public string Summary { get; set; } = default!;
    public string Content { get; set; } = default!;
    public string? CoverImageUrl { get; set; }
    public bool IsPublished { get; set; }
    public DateTime? PublishedAt { get; set; }
    public string PublishedAtPersian => this.PublishedAt?.ToShortPersianDateTimeString() ?? "";
    public string CreatedAtPersian => this.CreatedAt.ToShortPersianDateTimeString();
    public string UpdatedAtPersian => this.UpdatedAt.ToShortPersianDateTimeString();
    public int ViewCount { get; set; }
    public int CategoryId { get; set; }
    public string CategoryTitle { get; set; } = default!;

    // SEO
    public string? MetaTitle { get; set; }
    public string? MetaDescription { get; set; }
    public string? OgImageUrl { get; set; }

    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
    public bool IsCommentsEnabled { get; set; }
    public bool IsInEnglish { get; set; }

    public List<int> RelatedPosts { get; set; } = new List<int>();
    public List<int> TagIds { get; set; } = new List<int>();

    public int PostCommentsCount { get; set; }
}
