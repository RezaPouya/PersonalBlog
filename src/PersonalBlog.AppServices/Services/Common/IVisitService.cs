using PersonalBlog.AppServices.Dtos;

namespace PersonalBlog.AppServices.Services.Common;

public interface IVisitService
{
    Task RegisterVisitAsync(long postId, string ipAddress, string? userAgent,
        CancellationToken cancellationToken = default);

    Task<List<PopularPostDto>> GetMostVisitedPostsAsync(int count = 5,
        CancellationToken cancellationToken = default);

    Task<PostVisitStatsDto> GetStatsAsync(long postId, CancellationToken cancellationToken = default);
}
