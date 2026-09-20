using PersonalBlog.Domain.Entities.Posts;
using PersonalBlog.Domain.Exceptions;

namespace AppServices.Site.Posts;

public class GetPostBySlugQueryHandler(IPostRepository postRepository)
    : IQueryHandler<GetPostBySlugQuery, GetPostBySlugResult>
{
    public async Task<GetPostBySlugResult> Handle(GetPostBySlugQuery input, CancellationToken cancellationToken)
    {
        // نکته: این هندلر هم قبلاً فقط throw new NotImplementedException() بود.
        var p = await postRepository.GetPublishedBySlugAsync(input.Slug, cancellationToken)
            ?? throw new BusinessException("پست یافت نشد.");

        // ثبت بازدید (بدون بلاک‌کردن نمایش صفحه در صورت بروز خطا)
        try
        {
            await postRepository.IncrementViewCountAsync(p.Id, cancellationToken);
        }
        catch
        {
            // نمایش پست مهم‌تر از موفقیت ثبت بازدید است.
        }

        return new GetPostBySlugResult
        {
            Id = p.Id,
            Title = p.Title,
            Slug = p.Slug,
            Summary = p.Summary,
            Content = p.Content,
            CoverImageUrl = p.CoverImageUrl,
            PublishedAt = p.PublishedAt,
            CategoryTitle = p.CategoryTitle,
            CategorySlug = p.CategorySlug,
            ViewCount = p.ViewCount,
            IsCommentsEnabled = p.IsCommentsEnabled,
            MetaTitle = p.MetaTitle,
            MetaDescription = p.MetaDescription,
            Tags = p.Tags
        };
    }
}
