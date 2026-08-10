using AppServices.Base;
using PersonalBlog.Domain.Entities.Posts;
using PersonalBlog.Domain.Exceptions;

namespace AppServices.Site.Posts;

public class GetPostBySlugQueryHandler(IPostRepository postRepository)
    : IQueryHandler<GetPostBySlugQuery, GetPostBySlugResult>
{
    public async Task<GetPostBySlugResult> Invoke(GetPostBySlugQuery input, CancellationToken cancellationToken)
    {
        var p = await postRepository.GetPublishedBySlugAsync(input.Slug, cancellationToken)
            ?? throw new BusinessException("پست یافت نشد.");

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
