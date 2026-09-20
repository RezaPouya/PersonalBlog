using PersonalBlog.Domain.Entities.Comments;
using PersonalBlog.Domain.Entities.Comments.Dtos;
using Utilities.Dtos;

namespace AppServices.Admin.Comments.Grid;

public class GetCommentListAsGridQueryHandler(ICommentRepository commentRepository)
    : IQueryHandler<GetCommentListAsGridQuery, GridDataSourceResult<CommentGridDto>>
{
    public async Task<GridDataSourceResult<CommentGridDto>> Handle(GetCommentListAsGridQuery input, CancellationToken cancellationToken)
    {
        return await commentRepository.GetGridAsync(input, cancellationToken);
    }
}
