using PersonalBlog.Domain.Entities.Posts;
using PersonalBlog.Domain.Entities.Posts.Dtos;

namespace AppServices.Site.Posts;

public class SearchPostsQueryHandler(IPostRepository postRepository)
    : IQueryHandler<SearchPostsQuery, PostListPageDto>
{
    public async Task<PostListPageDto> Handle(SearchPostsQuery input, CancellationToken cancellationToken)
    {
        return await postRepository.SearchPublishedAsync(input.Query, input.Page, input.PageSize, cancellationToken);
    }
}
