using PersonalBlog.Domain.Commons;
using PersonalBlog.Domain.Entities.Tags.Dtos;
using Utilities.Dtos;

namespace PersonalBlog.Domain.Entities.Tags;

public interface ITagRepository : IRepository<Tag>
{
    Task<bool> IsExistsByTitleAsync(
        string title,
        int? id,
        CancellationToken cancellationToken);

    Task<bool> HasAnyPostAsync(
        int tagId,
        CancellationToken cancellationToken);

    Task<GridDataSourceResult<TagGridDto>> GetGridAsync(
        GridDataSourceRequest request,
        CancellationToken cancellationToken);

    Task<List<IdTitleDto<int>>> GetListForLookupAsync(
        CancellationToken cancellationToken);

    Task<List<SitemapUrlDto>> GetAllForSitemapAsync(CancellationToken cancellationToken);
}