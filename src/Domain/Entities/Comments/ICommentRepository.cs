using PersonalBlog.Domain.Commons;
using PersonalBlog.Domain.Entities.Comments.Dtos;
using Utilities.Dtos;

namespace PersonalBlog.Domain.Entities.Comments;

public interface ICommentRepository : IRepository<Comment>
{
    Task<GridDataSourceResult<CommentGridDto>> GetGridAsync(GridDataSourceRequest request, CancellationToken cancellationToken);
<<<<<<< HEAD
    Task<List<PublicCommentDto>> GetApprovedByPostIdAsync(int postId, CancellationToken cancellationToken);
=======
>>>>>>> 85b1d15fc1b3e1d14dce5e1b74d218fa26ad86b6
}
