namespace AppServices.Dtos;

public class CreateProjectInputDto
{
    public string Title { get; set; } = default!;
    public string Slug { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string? ImageUrl { get; set; }
    public string? LiveUrl { get; set; }
    public string? RepoUrl { get; set; }
    public string? TechnologiesCsv { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsFeatured { get; set; }
}
