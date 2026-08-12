using PersonalBlog.Domain.Commons;
using PersonalBlog.Domain.Entities.Categories.Dtos;
using PersonalBlog.Utilities.Dtos;
using Utilities.Dtos;

namespace PersonalBlog.Domain.Entities.Categories;

public interface ICategoryRepository : IRepository<Category>
{
    Task<bool> IsExistsByTitleAsync(string title, CancellationToken cancellationToken);
    Task<CategoryDot?> GetCategoryInfoByIdAsync(int id, CancellationToken cancellationToken);
    Task<GridDataSourceResult<CategoryGridDto>> GetCategoryGridAsync(GridDataSourceRequest request,
        CancellationToken cancellationToken);

    Task<List<IdTitleDto<int>>> GetListForLookupAsync(CancellationToken cancellationToken);
}
