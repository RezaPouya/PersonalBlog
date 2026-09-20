using DNTPersianUtils.Core;

namespace PersonalBlog.Domain.Entities.Projects.Dtos;

public class ProjectGridDto
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string Slug { get; set; } = default!;
    public string? ImageUrl { get; set; }
    public bool IsFeatured { get; set; }
    public bool IsInEnglish { get; set; }
    public int OrderInProjects { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedAtPersian => CreatedAt.ToShortPersianDateTimeString();
}
