using Microsoft.EntityFrameworkCore;
using PersonalBlog.AppServices.Dtos;
using PersonalBlog.Domain.Commons;
using PersonalBlog.Domain.Constants;
using PersonalBlog.Domain.Entities.Blog;

namespace PersonalBlog.AppServices.Services.Blog.Imps;

public class TagService(
    IRepository<Tag> repository,
    IUnitOfWork unitOfWork,
    ILocalCacheManager cache) : ITagService
{
    public async Task<List<TagDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await cache.GetOrCreateAsync(CacheKeys.TagsList, async () =>
        {
            return await repository.Query()
                .OrderBy(t => t.Title)
                .Select(t => new TagDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    Slug = t.Slug,
                    PostCount = t.PostTags.Count(pt => pt.Post.IsPublished && !pt.Post.IsDeleted)
                })
                .ToListAsync(cancellationToken);
        }, timeOutInSeconds: 300, cancellationToken);
    }

    public async Task<TagDto?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default)
    {
        return await repository.Query()
            .Where(t => t.Slug == slug)
            .Select(t => new TagDto
            {
                Id = t.Id,
                Title = t.Title,
                Slug = t.Slug,
                PostCount = t.PostTags.Count(pt => pt.Post.IsPublished && !pt.Post.IsDeleted)
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<long> GetOrCreateAsync(string title, CancellationToken cancellationToken = default)
    {
        var slug = Slugify(title);

        var existing = await repository.Query()
            .FirstOrDefaultAsync(t => t.Slug == slug, cancellationToken);

        if (existing != null)
            return existing.Id;

        var entity = new Tag { Title = title.Trim(), Slug = slug };
        repository.Add(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        cache.Remove(CacheKeys.TagsList);
        return entity.Id;
    }

    public async Task DeleteAsync(long id, CancellationToken cancellationToken = default)
    {
        var entity = await repository.FindByIdAsync(id);
        if (entity is null)
            return;

        repository.Delete(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        cache.Remove(CacheKeys.TagsList);
    }

    private static string Slugify(string title) =>
        title.Trim().Replace(" ", "-").ToLowerInvariant();
}
