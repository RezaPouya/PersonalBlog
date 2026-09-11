using Microsoft.EntityFrameworkCore;
using PersonalBlog.AppServices.Dtos;
using PersonalBlog.Domain.Commons;
using PersonalBlog.Domain.Constants;
using PersonalBlog.Domain.Entities.Blog;
using UAParser;

namespace PersonalBlog.AppServices.Services.Common.Imps;

/// <summary>
/// ثبت بازدید هر پست همراه با اطلاعات مرورگر/سیستم‌عامل/دستگاه با کتابخانه‌ی UAParser
/// (دقیقاً همان الگوی پروژه‌ی نمونه).
/// </summary>
public class VisitService(
    IRepository<Post> postRepository,
    IRepository<PostVisit> visitRepository,
    IUnitOfWork unitOfWork,
    ILocalCacheManager cache) : IVisitService
{
    public async Task RegisterVisitAsync(long postId, string ipAddress, string? userAgent,
        CancellationToken cancellationToken = default)
    {
        var post = await postRepository.FindByIdAsync(postId);
        if (post is null)
            return;

        var clientInfo = string.IsNullOrWhiteSpace(userAgent)
            ? null
            : Parser.GetDefault().Parse(userAgent);

        visitRepository.Add(new PostVisit
        {
            PostId = postId,
            VisitedAt = DateTime.Now,
            IpAddress = ipAddress,
            UserAgent = userAgent,
            Browser = clientInfo?.UA.Family,
            OS = clientInfo?.OS.Family,
            Device = clientInfo?.Device.Family
        });

        post.ViewCount++;
        postRepository.Update(post);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        cache.Remove(CacheKeys.PopularPosts);
    }

    public async Task<List<PopularPostDto>> GetMostVisitedPostsAsync(int count = 5,
        CancellationToken cancellationToken = default)
    {
        return await cache.GetOrCreateAsync(CacheKeys.PopularPosts, async () =>
        {
            return await postRepository.Query()
                .Where(p => p.IsPublished && !p.IsDeleted)
                .OrderByDescending(p => p.ViewCount)
                .Take(count)
                .Select(p => new PopularPostDto
                {
                    PostId = p.Id,
                    Title = p.Title,
                    Slug = p.Slug,
                    ViewCount = p.ViewCount
                })
                .ToListAsync(cancellationToken);
        }, timeOutInSeconds: 300, cancellationToken);
    }

    public async Task<PostVisitStatsDto> GetStatsAsync(long postId, CancellationToken cancellationToken = default)
    {
        var visits = await visitRepository.Query()
            .Where(v => v.PostId == postId)
            .ToListAsync(cancellationToken);

        return new PostVisitStatsDto
        {
            PostId = postId,
            TotalVisits = visits.Count,
            ByBrowser = visits.Where(v => v.Browser != null)
                .GroupBy(v => v.Browser!).ToDictionary(g => g.Key, g => g.Count()),
            ByOS = visits.Where(v => v.OS != null)
                .GroupBy(v => v.OS!).ToDictionary(g => g.Key, g => g.Count()),
            ByDevice = visits.Where(v => v.Device != null)
                .GroupBy(v => v.Device!).ToDictionary(g => g.Key, g => g.Count())
        };
    }
}
