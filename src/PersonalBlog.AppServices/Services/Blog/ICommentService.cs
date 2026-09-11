using PersonalBlog.AppServices.Dtos;

namespace PersonalBlog.AppServices.Services.Blog;

public interface ICommentService
{
    Task<long> SubmitAsync(CreateCommentInputDto input, string ipAddress,
        CancellationToken cancellationToken = default);

    Task<List<CommentDto>> GetPendingAsync(CancellationToken cancellationToken = default);
    Task ApproveAsync(long id, CancellationToken cancellationToken = default);
    Task RejectAsync(long id, CancellationToken cancellationToken = default);
    Task DeleteAsync(long id, CancellationToken cancellationToken = default);
}
