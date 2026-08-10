using AppServices.Base;
using PersonalBlog.Domain.Entities.Categories;
using PersonalBlog.Utilities.Dtos;

namespace AppServices.Admin.Categories.Grid;

public class GetCategoryListAsGridQueryHandler(ICategoryRepository categoryRepository)
: IQueryHandler<GetCategoryListAsGridQuery, GridDataSourceResult<GetCategoryGridResult>>
{
    public async Task<GridDataSourceResult<GetCategoryGridResult>> Invoke(GetCategoryListAsGridQuery input,
        CancellationToken cancellationToken)
    {
        var gridResult = await categoryRepository.GetCategoryGridAsync(input, cancellationToken);

        var mappedData = gridResult.Data
            .Select(dto => new GetCategoryGridResult
            {
                Id = dto.Id,
                CreatedAt = dto.CreatedAt,
                UpdatedAt = dto.UpdatedAt,
                Title = dto.Title,
                IsInEnglish = dto.IsInEnglish,
                PostsCount = dto.PostsCount
            })
            .ToList();

        return new GridDataSourceResult<GetCategoryGridResult>
        {
            Page = gridResult.Page,
            PageSize = gridResult.PageSize,
            TotalPages = gridResult.TotalPages,
            Totals = gridResult.Totals,
            Data = mappedData
        };
    }
}
