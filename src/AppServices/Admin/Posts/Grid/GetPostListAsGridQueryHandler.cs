using AppServices.Base;
using PersonalBlog.Domain.Entities.Posts;
using PersonalBlog.Utilities.Dtos;

namespace AppServices.Admin.Posts.Grid;

public class GetPostListAsGridQueryHandler(IPostRepository postRepository)
    : IQueryHandler<GetPostListAsGridQuery, GridDataSourceResult<GetPostGridResult>>
{
    public async Task<GridDataSourceResult<GetPostGridResult>> Invoke(GetPostListAsGridQuery input, CancellationToken cancellationToken)
    {
        var gridResult = await postRepository.GetPostGridAsync(input, cancellationToken);

        var mapped = gridResult.Data.Select(dto => new GetPostGridResult
        {
            Id = dto.Id,
            CreatedAt = dto.CreatedAt,
            UpdatedAt = dto.UpdatedAt,
            Title = dto.Title,
            Slug = dto.Slug,
            IsPublished = dto.IsPublished,
            PublishedAt = dto.PublishedAt,
            CategoryId = dto.CategoryId,
            CategoryTitle = dto.CategoryTitle,
            ViewCount = dto.ViewCount,
            IsInEnglish = dto.IsInEnglish
        }).ToList();

        return new GridDataSourceResult<GetPostGridResult>
        {
            Page = gridResult.Page,
            PageSize = gridResult.PageSize,
            TotalPages = gridResult.TotalPages,
            Totals = gridResult.Totals,
            Data = mapped
        };
    }
}
