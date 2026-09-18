using PersonalBlog.Domain.Entities.Categories.Dtos;
using Utilities.Dtos;

namespace AppServices.Admin.Categories.Grid;

public class GetCategoryListAsGridQuery : GridDataSourceRequest, IQuery<GridDataSourceResult<CategoryGridDto>>
{
}
