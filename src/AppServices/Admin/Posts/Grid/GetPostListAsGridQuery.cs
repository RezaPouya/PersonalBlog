using PersonalBlog.Domain.Entities.Posts.Dtos;
using Utilities.Dtos;

namespace AppServices.Admin.Posts.Grid;

public class GetPostListAsGridQuery : GridDataSourceRequest, IQuery<GridDataSourceResult<PostGridDto>>
{
}
