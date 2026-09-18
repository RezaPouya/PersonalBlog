namespace AppServices.Site.Posts;

public class GetPostBySlugQuery : IQuery<GetPostBySlugResult>
{
    public string Slug { get; set; } = default!;
}
