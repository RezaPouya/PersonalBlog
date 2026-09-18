namespace AppServices.Site.Posts;

public class GetLatestPostsQuery : IQuery<List<GetLatestPostsResult>>
{
    public int Count { get; set; } = 10;
    public bool? IsInEnglish { get; set; }
}
