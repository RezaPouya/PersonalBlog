using PersonalBlog.Domain.Entities.Comments.Dtos;
using Utilities.Dtos;

namespace AppServices.Admin.Comments.Grid;

public class GetCommentListAsGridQuery : GridDataSourceRequest, IQuery<GridDataSourceResult<CommentGridDto>>
{
}
