namespace AppServices.Site.Posts;

public class GetLatestPostsQuery
{
    public int Count { get; set; } = 10;
    public bool? IsInEnglish { get; set; }
}
