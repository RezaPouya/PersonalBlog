namespace PersonalBlog.AppServices.Dtos;

public class PopularPostDto
{
    public long PostId { get; set; }
    public string Title { get; set; } = default!;
    public string Slug { get; set; } = default!;
    public int ViewCount { get; set; }
}