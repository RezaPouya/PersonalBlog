using PersonalBlog.Domain.Entities.Comments;
using PersonalBlog.Domain.Entities.Comments.Dtos;
using Utilities.Dtos;

namespace Infrastructure.Persistence.SqlServer.Repositories;

public class CommentRepository(AppDbContext dbContext) : RepositoryBase<Comment>(dbContext), ICommentRepository
{
    public async Task<GridDataSourceResult<CommentGridDto>> GetGridAsync(GridDataSourceRequest request, CancellationToken cancellationToken)
    {
        var query = base.DbContext.Comments.AsNoTracking()
            .Select(c => new CommentGridDto
            {
                Id = c.Id,
                PostId = c.PostId,
                PostTitle = c.Post.Title,
                DisplayName = c.DisplayName,
                Email = c.Email,
                Content = c.Content,
                IsApproved = c.IsApproved,
                IsSpam = c.IsSpam,
                CreatedAt = c.CreatedAt
            });

        return await query.ToDataSourceResult(request, cancellationToken);
    }

    public async Task<List<PublicCommentDto>> GetApprovedByPostIdAsync(int postId, CancellationToken cancellationToken)
    {
        return await base.DbContext.Comments.AsNoTracking()
            .Where(c => c.PostId == postId && c.ParentCommentId == null && c.IsApproved && !c.IsSpam)
            .OrderBy(c => c.CreatedAt)
            .Select(c => new PublicCommentDto
            {
                Id = c.Id,
                DisplayName = c.DisplayName,
                Content = c.Content,
                CreatedAt = c.CreatedAt,
                Replies = c.Replies
                    .Where(r => r.IsApproved && !r.IsSpam)
                    .OrderBy(r => r.CreatedAt)
                    .Select(r => new PublicCommentDto
                    {
                        Id = r.Id,
                        DisplayName = r.DisplayName,
                        Content = r.Content,
                        CreatedAt = r.CreatedAt,
                        Replies = new List<PublicCommentDto>()
                    })
                    .ToList()
            })
            .ToListAsync(cancellationToken);
    }
}
