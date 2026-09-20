using PersonalBlog.Domain.Entities.Posts;

namespace AppServices.Site.Posts;

public class GetLatestPostsQueryHandler(IPostRepository postRepository)
    : IQueryHandler<GetLatestPostsQuery, List<GetLatestPostsResult>>
{
    public async Task<List<GetLatestPostsResult>> Handle(GetLatestPostsQuery input, CancellationToken cancellationToken)
    {
        // نکته: این هندلر قبلاً فقط throw new NotImplementedException() بود (کد واقعی کامنت شده بود)
        // یعنی صفحه‌ی اصلی سایت هیچ‌وقت واقعاً پست نشان نمی‌داد.
        var items = await postRepository.GetLatestPublishedAsync(input.Count, input.IsInEnglish, cancellationToken);

        return items.Select(p => new GetLatestPostsResult
        {
            Id = p.Id,
            Title = p.Title,
            Slug = p.Slug,
            Summary = p.Summary,
            CoverImageUrl = p.CoverImageUrl,
            PublishedAt = p.PublishedAt,
            CategoryTitle = p.CategoryTitle,
            CategorySlug = p.CategorySlug,
            ViewCount = p.ViewCount
        }).ToList();
    }
}
