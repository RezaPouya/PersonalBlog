using PersonalBlog.Domain.Commons;
using PersonalBlog.Domain.Entities.Posts.Dtos;
using PersonalBlog.Utilities.Dtos;

namespace PersonalBlog.Domain.Entities.Posts;

public interface IPostRepository : IRepository<Post>
{
    Task<bool> DoesCategoryHaveAnyPost(int categoryId, CancellationToken cancellationToken);
    Task<bool> IsExistsBySlugAsync(string slug, int? id, CancellationToken cancellationToken);
    Task<PostDto?> GetInfoByIdAsync(int id, CancellationToken cancellationToken);
    Task<GridDataSourceResult<PostGridDto>> GetGridAsync(GridDataSourceRequest request, CancellationToken cancellationToken);
    Task ReplaceTagsAsync(int postId, IEnumerable<int> tagIds, CancellationToken cancellationToken);
}
