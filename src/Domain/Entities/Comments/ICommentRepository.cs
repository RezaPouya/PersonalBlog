using PersonalBlog.Domain.Commons;
using PersonalBlog.Domain.Entities.Comments.Dtos;
using Utilities.Dtos;

namespace PersonalBlog.Domain.Entities.Comments;

public interface ICommentRepository : IRepository<Comment>
{
    Task<GridDataSourceResult<CommentGridDto>> GetGridAsync(GridDataSourceRequest request, CancellationToken cancellationToken);
}
