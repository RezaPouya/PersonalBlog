using PersonalBlog.Domain.Entities.Posts;
using Utilities.Dtos;

namespace AppServices.Admin.Lookups;

public class GetPostsForCourseLookupQueryHandler(IPostRepository postRepository)
    : IQueryHandler<GetPostsForCourseLookupQuery, List<IdTitleDto<int>>>
{
    public async Task<List<IdTitleDto<int>>> Handle(GetPostsForCourseLookupQuery input, CancellationToken cancellationToken)
    {
        return await postRepository.GetListForLookupAsync(cancellationToken);
    }
}
