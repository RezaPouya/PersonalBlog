using PersonalBlog.Domain.Entities.Posts;
using PersonalBlog.Domain.Entities.Posts.Dtos;

namespace AppServices.Site.Posts;

public class GetPublishedPostsQueryHandler(IPostRepository postRepository)
    : IQueryHandler<GetPublishedPostsQuery, PostListPageDto>
{
    public async Task<PostListPageDto> Handle(GetPublishedPostsQuery input, CancellationToken cancellationToken)
    {
        return await postRepository.GetPublishedListAsync(input.Page, input.PageSize, input.CategoryId, input.TagId, cancellationToken);
    }
}
