using Microsoft.EntityFrameworkCore;
using PersonalBlog.AppServices.Dtos;
using PersonalBlog.Domain.Commons;
using PersonalBlog.Domain.Constants;
using PersonalBlog.Domain.Entities.Blog;

namespace PersonalBlog.AppServices.Services.Blog.Imps;

public class CategoryService(
    IRepository<Category> repository,
    IUnitOfWork unitOfWork,
    ILocalCacheManager cache) : ICategoryService
{
    public async Task<List<CategoryDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await cache.GetOrCreateAsync(CacheKeys.CategoriesList, async () =>
        {
            return await repository.Query()
                .OrderBy(c => c.Title)
                .Select(c => new CategoryDto
                {
                    Id = c.Id,
                    Title = c.Title,
                    Slug = c.Slug,
                    Description = c.Description,
                    PostCount = c.Posts.Count(p => p.IsPublished && !p.IsDeleted)
                })
                .ToListAsync(cancellationToken);
        }, timeOutInSeconds: 300, cancellationToken);
    }

    public async Task<CategoryDto?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default)
    {
        return await repository.Query()
            .Where(c => c.Slug == slug)
            .Select(c => new CategoryDto
            {
                Id = c.Id,
                Title = c.Title,
                Slug = c.Slug,
                Description = c.Description,
                PostCount = c.Posts.Count(p => p.IsPublished && !p.IsDeleted)
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<long> CreateAsync(CreateCategoryInputDto input, CancellationToken cancellationToken = default)
    {
        var entity = new Category
        {
            Title = input.Title,
            Slug = input.Slug,
            Description = input.Description
        };

        repository.Add(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        cache.Remove(CacheKeys.CategoriesList);
        return entity.Id;
    }

    public async Task UpdateAsync(UpdateCategoryInputDto input, CancellationToken cancellationToken = default)
    {
        var entity = await repository.FindByIdAsync(input.Id)
            ?? throw new KeyNotFoundException("دسته‌بندی یافت نشد.");

        entity.Title = input.Title;
        entity.Slug = input.Slug;
        entity.Description = input.Description;

        repository.Update(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        cache.Remove(CacheKeys.CategoriesList);
    }

    public async Task DeleteAsync(long id, CancellationToken cancellationToken = default)
    {
        var entity = await repository.FindByIdAsync(id);
        if (entity is null)
            return;

        repository.Delete(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        cache.Remove(CacheKeys.CategoriesList);
    }
}
