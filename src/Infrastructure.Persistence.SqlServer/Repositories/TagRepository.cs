using PersonalBlog.Domain.Entities.Tags;
using PersonalBlog.Domain.Entities.Tags.Dtos;
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

    public async Task<List<SitemapUrlDto>> GetAllForSitemapAsync(CancellationToken cancellationToken)
    {
        // فقط برچسب‌هایی که حداقل یک پست منتشرشده دارند وارد sitemap می‌شوند
        // تا صفحه‌ی خالی /tag/{id} به گوگل معرفی نشود.
        // نکته: Tag اسلاگ ندارد، پس شناسه‌ی عددی را در همان فیلد Slug می‌گذاریم
        // چون مسیر سایت هم /tag/{id:int} است، نه /tag/{slug}.
        return await DbContext.Tags.AsNoTracking()
            .Where(t => t.PostTags.Any(pt => pt.Post.IsPublished && !pt.Post.IsDeleted))
            .Select(t => new SitemapUrlDto
            {
                Slug = t.Id.ToString(),
                UpdatedAt = t.UpdatedAt
            })
            .ToListAsync(cancellationToken);
    }
}
