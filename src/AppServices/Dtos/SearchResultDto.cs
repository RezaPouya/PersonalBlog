namespace AppServices.Dtos;

public class SearchResultDto { public long PostId { get; set; } public string Title { get; set; } = default!; public string Slug { get; set; } = default!; public string Summary { get; set; } = default!; public string CategoryTitle { get; set; } = default!; public DateTime? PublishedAt { get; set; } }
