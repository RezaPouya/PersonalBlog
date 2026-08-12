using PersonalBlog.Domain.Entities.Posts;
using PersonalBlog.Domain.Entities.Posts.Dtos;
using PersonalBlog.Utilities.Dtos;

namespace AppServices.Admin.Posts.Grid;

public class GetPostListAsGridQueryHandler(IPostRepository postRepository) : IQueryHandler<GetPostListAsGridQuery, GridDataSourceResult<PostGridDto>>
{
    public async Task<GridDataSourceResult<PostGridDto>> Invoke(GetPostListAsGridQuery input, CancellationToken cancellationToken)
    {
        GridDataSourceResult<PostGridDto> gridResult = await postRepository.GetGridAsync(input, cancellationToken);

        return gridResult;
    }
}
