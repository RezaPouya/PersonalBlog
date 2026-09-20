namespace PersonalBlog.Domain.Entities.Projects.Dtos;

/// <summary>برای فرم ویرایش پروژه در پنل ادمین.</summary>
public class ProjectDto
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string Slug { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string? ImageUrl { get; set; }
    public string? LiveUrl { get; set; }
    public string? RepoUrl { get; set; }
    public string? TechnologiesCsv { get; set; }
    public int OrderInProjects { get; set; }
    public bool IsFeatured { get; set; }
    public bool IsInEnglish { get; set; }
}
