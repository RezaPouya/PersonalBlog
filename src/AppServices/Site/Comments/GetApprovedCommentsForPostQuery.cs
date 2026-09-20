using PersonalBlog.Domain.Entities.Comments.Dtos;

namespace AppServices.Site.Comments;

public class GetApprovedCommentsForPostQuery : IQuery<List<PublicCommentDto>>
{
    public int PostId { get; set; }
}
