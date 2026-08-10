using PersonalBlog.Domain.Commons;

namespace PersonalBlog.Domain.Entities.Posts;

public interface IPostRepository : IRepository<Post>
{
    Task<bool> DoesCategoryHaveAnyPost(int categoyryId, CancellationToken cancellationToken);
}
