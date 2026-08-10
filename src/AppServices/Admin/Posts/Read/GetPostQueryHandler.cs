using AppServices.Base;
using PersonalBlog.Domain.Entities.Posts;
using PersonalBlog.Domain.Exceptions;

namespace AppServices.Admin.Posts.Read;

public class GetPostQueryHandler(IPostRepository postRepository)
    : IQueryHandler<GetPostQuery, GetPostResult>
{
    public async Task<GetPostResult> Invoke(GetPostQuery request, CancellationToken cancellationToken)
    {
        var record = await postRepository.GetPostInfoByIdAsync(request.Id, cancellationToken)
            ?? throw new BusinessException("پست با این شناسه یافت نشد.");

        return new GetPostResult
        {
            Id = record.Id,
            CreatedAt = record.CreatedAt,
            UpdatedAt = record.UpdatedAt,
            Title = record.Title,
            Slug = record.Slug,
            Summary = record.Summary,
            Content = record.Content,
            CoverImageUrl = record.CoverImageUrl,
            IsPublished = record.IsPublished,
            PublishedAt = record.PublishedAt,
            CategoryId = record.CategoryId,
            CategoryTitle = record.CategoryTitle,
            IsCommentsEnabled = record.IsCommentsEnabled,
            IsInEnglish = record.IsInEnglish,
            MetaTitle = record.MetaTitle,
            MetaDescription = record.MetaDescription,
            OgImageUrl = record.OgImageUrl,
            ViewCount = record.ViewCount,
            TinyUrl = record.TinyUrl,
            TagIds = record.TagIds
        };
    }
}
