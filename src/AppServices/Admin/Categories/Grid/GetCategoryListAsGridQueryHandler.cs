using PersonalBlog.Domain.Entities.Categories;
using PersonalBlog.Domain.Entities.Categories.Dtos;
using Utilities.Dtos;

namespace AppServices.Admin.Categories.Grid;

public class GetCategoryListAsGridQueryHandler(ICategoryRepository categoryRepository) : IQueryHandler<GetCategoryListAsGridQuery, GridDataSourceResult<CategoryGridDto>>
{
    public async Task<GridDataSourceResult<CategoryGridDto>> Handle(GetCategoryListAsGridQuery input,
        CancellationToken cancellationToken)
    {
        GridDataSourceResult<CategoryGridDto> gridResult =
            await categoryRepository.GetCategoryGridAsync(input, cancellationToken);

        return gridResult;
    }
}
