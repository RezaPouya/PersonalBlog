using PersonalBlog.Domain.Entities.Posts.Dtos;

namespace AppServices.Site.Posts;

public class GetPublishedPostsQuery : IQuery<PostListPageDto>
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public int? CategoryId { get; set; }
    public int? TagId { get; set; }
}
