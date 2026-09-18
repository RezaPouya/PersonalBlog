using PersonalBlog.Domain.Entities.Tags.Dtos;
using Utilities.Dtos;

namespace AppServices.Admin.Tags.Grid;

public class GetTagListAsGridQuery : GridDataSourceRequest, IQuery<GridDataSourceResult<TagGridDto>>
{
}