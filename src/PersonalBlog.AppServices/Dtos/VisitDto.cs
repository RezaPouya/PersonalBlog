namespace PersonalBlog.AppServices.Dtos;

public class PopularPostDto
{
    public long PostId { get; set; }
    public string Title { get; set; } = default!;
    public string Slug { get; set; } = default!;
    public int ViewCount { get; set; }
}

public class PostVisitStatsDto
{
    public long PostId { get; set; }
    public int TotalVisits { get; set; }
    public Dictionary<string, int> ByBrowser { get; set; } = new();
    public Dictionary<string, int> ByOS { get; set; } = new();
    public Dictionary<string, int> ByDevice { get; set; } = new();
}
