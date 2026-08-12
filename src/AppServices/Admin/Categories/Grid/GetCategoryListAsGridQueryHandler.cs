using PersonalBlog.Domain.Entities.Categories;
using PersonalBlog.Domain.Entities.Categories.Dtos;
using PersonalBlog.Utilities.Dtos;

namespace AppServices.Admin.Categories.Grid;

public class GetCategoryListAsGridQueryHandler(ICategoryRepository categoryRepository) : IQueryHandler<GetCategoryListAsGridQuery, GridDataSourceResult<CategoryGridDto>>
{
    public async Task<GridDataSourceResult<CategoryGridDto>> Invoke(GetCategoryListAsGridQuery input,
        CancellationToken cancellationToken)
    {
        GridDataSourceResult<CategoryGridDto> gridResult =
            await categoryRepository.GetCategoryGridAsync(input, cancellationToken);

        return gridResult;
    }
}
