using PersonalBlog.Domain.Entities.Tags;
using Utilities.Dtos;

namespace Infrastructure.Persistence.SqlServer.Repositories;

public class TagRepository(AppDbContext dbContext) : RepositoryBase<Tag>(dbContext), ITagRepository
{
    public async Task<List<IdTitleDto<int>>> GetListForLookupAsync(CancellationToken cancellationToken)
    {
        List<IdTitleDto<int>> results = await base.DbContext.Tags.AsNoTracking().
            Select(p => new IdTitleDto<int> { Id = p.Id, Title = p.Title })
            .OrderBy(p => p.Title)
            .ToListAsync(cancellationToken);

        return results;
    }

    public async Task<Tag?> GetByTitleAsync(string title, CancellationToken cancellationToken)
    {
        Tag? results = await base.DbContext.Tags.AsNoTracking().FirstOrDefaultAsync(p => p.Title == title, cancellationToken);
        return results;
    }
}
