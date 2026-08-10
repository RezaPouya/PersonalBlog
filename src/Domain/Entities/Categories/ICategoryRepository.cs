using PersonalBlog.Domain.Commons;
using PersonalBlog.Domain.Entities.Categories.Dtos;
using PersonalBlog.Utilities.Dtos;

namespace PersonalBlog.Domain.Entities.Categories;

public interface ICategoryRepository : IRepository<Category>
{
    Task<bool> IsExistsByTitleAsync(string title, CancellationToken cancellationToken);
    Task<CategoryDbDto?> GetCategoryInfoByIdAsync(int id, CancellationToken cancellationToken);
    Task<GridDataSourceResult<CategoryGridDbDto>> GetCategoryGridAsync(GridDataSourceRequest request,
        CancellationToken cancellationToken);
}
