using PersonalBlog.Domain.Entities.Posts;
using PersonalBlog.Domain.Entities.Posts.Dtos;
using PersonalBlog.Domain.Exceptions;

namespace AppServices.Admin.Posts.Read;

public class GetPostQueryHandler(IPostRepository postRepository)
    : IQueryHandler<GetPostQuery, PostDto>
{
    public async Task<PostDto> Invoke(GetPostQuery request, CancellationToken cancellationToken)
    {
        var record = await postRepository.GetInfoByIdAsync(request.Id, cancellationToken)
            ?? throw new BusinessException("پست با این شناسه یافت نشد.");

        return record;
    }
}
