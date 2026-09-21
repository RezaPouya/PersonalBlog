using PersonalBlog.Domain.Entities.Posts.Dtos;

namespace AppServices.Site.Posts;

public class SearchPostsQuery : IQuery<PostListPageDto>
{
    public string Query { get; set; } = "";
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
