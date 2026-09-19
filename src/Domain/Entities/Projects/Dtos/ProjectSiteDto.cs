namespace PersonalBlog.Domain.Entities.Projects.Dtos;

/// <summary>برای نمایش عمومی در صفحه‌ی «درباره من» / پروژه‌ها.</summary>
public class ProjectSiteDto
{
    public string Title { get; set; } = default!;
    public string Slug { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string? ImageUrl { get; set; }
    public string? LiveUrl { get; set; }
    public string? RepoUrl { get; set; }
    public string? TechnologiesCsv { get; set; }
    public bool IsFeatured { get; set; }
}
