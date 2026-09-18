namespace AppServices.Dtos;

// --- Projects ---
public class ProjectDto { public long Id { get; set; } public string Title { get; set; } = default!; public string Slug { get; set; } = default!; public string Description { get; set; } = default!; public string? ImageUrl { get; set; } public string? LiveUrl { get; set; } public string? RepoUrl { get; set; } public List<string> Technologies { get; set; } = new(); public bool IsFeatured { get; set; } }
