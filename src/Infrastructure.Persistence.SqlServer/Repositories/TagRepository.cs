using PersonalBlog.Domain.Entities.Tags;
using PersonalBlog.Domain.Entities.Tags.Dtos;
using PersonalBlog.Utilities.Dtos;
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

    public async Task<bool> IsExistsByTitleAsync(
       string title,
       int? id,
       CancellationToken cancellationToken)
    {
        var query = DbContext.Tags
            .AsNoTracking()
            .Where(x => x.Title == title);

        if (id.HasValue)
            query = query.Where(x => x.Id != id.Value);

        return await query.AnyAsync(cancellationToken);
    }

    public async Task<bool> HasAnyPostAsync(
        int tagId,
        CancellationToken cancellationToken)
    {
        return await DbContext.PostTags
            .AsNoTracking()
            .AnyAsync(x => x.TagId == tagId, cancellationToken);
    }

    public async Task<GridDataSourceResult<TagGridDto>> GetGridAsync(
        GridDataSourceRequest request,
        CancellationToken cancellationToken)
    {
        var query = DbContext.Tags
            .AsNoTracking()
            .Select(x => new TagGridDto
            {
                Id = x.Id,
                Title = x.Title,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                PostsCount = x.PostTags.Count()
            });

        return await query.ToDataSourceResult(
            request,
            cancellationToken);
    }
}
