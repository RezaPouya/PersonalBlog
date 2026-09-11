using PersonalBlog.AppServices.Dtos;

namespace PersonalBlog.AppServices.Services.Blog;

public interface IPostService
{
    Task<(List<PostListItemDto> Items, int TotalCount)> GetListAsync(PostListFilterDto filter,
        CancellationToken cancellationToken = default);

    Task<PostDetailDto?> GetBySlugAsync(string slug, bool onlyApprovedComments = true,
        CancellationToken cancellationToken = default);

    Task<PostDetailDto?> GetByIdAsync(long id, CancellationToken cancellationToken = default);

    Task<long> CreateAsync(CreatePostInputDto input, long? currentUserId,
        CancellationToken cancellationToken = default);

    Task UpdateAsync(UpdatePostInputDto input, long? currentUserId,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(long id, long? currentUserId, CancellationToken cancellationToken = default);

    Task<bool> IsSlugAvailableAsync(string slug, long? exceptPostId = null,
        CancellationToken cancellationToken = default);
}
