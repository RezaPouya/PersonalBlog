using PersonalBlog.Domain.Entities.Posts;

namespace AppServices.Site.Posts;

public class GetLatestPostsQueryHandler(IPostRepository postRepository)
    : IQueryHandler<GetLatestPostsQuery, List<GetLatestPostsResult>>
{
    public async Task<List<GetLatestPostsResult>> Handle(GetLatestPostsQuery input, CancellationToken cancellationToken)
    {
<<<<<<< HEAD
        // نکته: این هندلر قبلاً فقط throw new NotImplementedException() بود (کد واقعی کامنت شده بود)
        // یعنی صفحه‌ی اصلی سایت هیچ‌وقت واقعاً پست نشان نمی‌داد.
=======
>>>>>>> 85b1d15fc1b3e1d14dce5e1b74d218fa26ad86b6
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
<<<<<<< HEAD
            CategorySlug = p.CategorySlug,
=======
>>>>>>> 85b1d15fc1b3e1d14dce5e1b74d218fa26ad86b6
            ViewCount = p.ViewCount
        }).ToList();
    }
}
