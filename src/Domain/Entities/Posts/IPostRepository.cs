using PersonalBlog.Domain.Commons;
using PersonalBlog.Domain.Entities.Posts.Dtos;
using Utilities.Dtos;

namespace PersonalBlog.Domain.Entities.Posts;

public interface IPostRepository : IRepository<Post>
{
    Task<bool> DoesCategoryHaveAnyPost(int categoryId, CancellationToken cancellationToken);
    Task<bool> IsExistsBySlugAsync(string slug, int? id, CancellationToken cancellationToken);
    Task<PostDto?> GetInfoByIdAsync(int id, CancellationToken cancellationToken);
    Task<GridDataSourceResult<PostGridDto>> GetGridAsync(GridDataSourceRequest request, CancellationToken cancellationToken);
    Task ReplaceTagsAsync(int postId, IEnumerable<int> tagIds, CancellationToken cancellationToken);
    Task<List<IdTitleDto<int>>> GetListForLookupAsync(CancellationToken cancellationToken);
<<<<<<< HEAD

    // ============ سمت سایت عمومی ============
    // نکته مهم: این سه متد قبلاً به‌صورت throw new NotImplementedException() در Handler ها
    // جا گذاشته شده بودند (کدشان کامنت بود) و صفحه‌ی اصلی/جزئیات پست هیچ‌وقت واقعاً کار نمی‌کرد.
    Task<List<PostSummaryDto>> GetLatestPublishedAsync(int count, bool? isInEnglish, CancellationToken cancellationToken);
    Task<PostSiteDetailDto?> GetPublishedBySlugAsync(string slug, CancellationToken cancellationToken);
    Task<PostListPageDto> GetPublishedListAsync(int page, int pageSize, int? categoryId, int? tagId, CancellationToken cancellationToken);
    Task IncrementViewCountAsync(int postId, CancellationToken cancellationToken);
    Task<List<SitemapUrlDto>> GetAllPublishedForSitemapAsync(CancellationToken cancellationToken);
    Task<List<PostSummaryDto>> GetLatestForFeedAsync(int count, CancellationToken cancellationToken);
=======
    Task<List<PostDto>> GetLatestPublishedAsync(int count, bool? isInEnglish, CancellationToken cancellationToken);
    Task<PostDto?> GetPublishedBySlugAsync(string slug, CancellationToken cancellationToken);
>>>>>>> 85b1d15fc1b3e1d14dce5e1b74d218fa26ad86b6
}
