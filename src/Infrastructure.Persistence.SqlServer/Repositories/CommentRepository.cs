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
}
