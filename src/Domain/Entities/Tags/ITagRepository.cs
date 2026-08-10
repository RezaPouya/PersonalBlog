using PersonalBlog.Domain.Commons;

namespace PersonalBlog.Domain.Entities.Tags;

public interface ITagRepository : IRepository<Tag>
{
    Task<bool> IsExistsByTitleAsync(string title, CancellationToken cancellationToken);
}
