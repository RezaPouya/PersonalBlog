using PersonalBlog.Domain.Entities.Comments;
using PersonalBlog.Domain.Entities.Comments.Dtos;

namespace AppServices.Site.Comments;

public class GetApprovedCommentsForPostQueryHandler(ICommentRepository commentRepository)
    : IQueryHandler<GetApprovedCommentsForPostQuery, List<PublicCommentDto>>
{
    public async Task<List<PublicCommentDto>> Handle(GetApprovedCommentsForPostQuery input, CancellationToken cancellationToken)
    {
        return await commentRepository.GetApprovedByPostIdAsync(input.PostId, cancellationToken);
    }
}
