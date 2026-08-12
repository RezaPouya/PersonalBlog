using PersonalBlog.Domain.Commons;
using Utilities.Dtos;

namespace PersonalBlog.Domain.Entities.Tags;

public interface ITagRepository : IRepository<Tag>
{
    Task<Tag?> GetByTitleAsync(string title, CancellationToken cancellationToken);

    Task<List<IdTitleDto<int>>> GetListForLookupAsync(CancellationToken cancellationToken);
}
