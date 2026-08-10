namespace PersonalBlog.Domain.Entities.Projects;

public class Project : EntityBase
{
    public Project()
    {
        this.TinyUrl = "pr_" + Guid.CreateVersion7().ToString("N").Substring(0, 4).ToLower();
    }

    public string Title { get; set; } = default!;
    public string Slug { get; set; } = default!;
    public string TinyUrl { get; set; }
    public string Description { get; set; } = default!;
    public string? ImageUrl { get; set; }
    public string? LiveUrl { get; set; }
    public string? RepoUrl { get; set; }
    public string? TechnologiesCsv { get; set; }   // مثال: "Blazor,EF Core,SQL Server"
    public int OrderInProjects { get; set; }
    public bool IsFeatured { get; set; }
    public bool IsInEnglish { get; set; }
}
