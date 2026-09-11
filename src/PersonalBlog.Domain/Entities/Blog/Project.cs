using PersonalBlog.Domain.Commons.Base;

namespace PersonalBlog.Domain.Entities.Blog;

public class Project : EntityBase
{
    public string Title { get; set; } = default!;
    public string Slug { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string? ImageUrl { get; set; }
    public string? LiveUrl { get; set; }
    public string? RepoUrl { get; set; }
    public string? TechnologiesCsv { get; set; }   // مثال: "Blazor,EF Core,SQL Server"
    public int DisplayOrder { get; set; }
    public bool IsFeatured { get; set; }
}
