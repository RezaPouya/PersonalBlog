using Microsoft.EntityFrameworkCore;
using PersonalBlog.AppServices.Dtos;
using PersonalBlog.Domain.Commons;
using PersonalBlog.Domain.Constants;
using PersonalBlog.Domain.Entities.Blog;

namespace PersonalBlog.AppServices.Services.Blog.Imps;

public class CourseService(
    IRepository<Course> repository,
    IUnitOfWork unitOfWork,
    ILocalCacheManager cache) : ICourseService
{
    public async Task<List<CourseDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await cache.GetOrCreateAsync(CacheKeys.CoursesList, async () =>
        {
            return await repository.Query()
                .Where(c => c.IsPublished)
                .OrderBy(c => c.Title)
                .Select(c => new CourseDto
                {
                    Id = c.Id,
                    Title = c.Title,
                    Slug = c.Slug,
                    Description = c.Description,
                    CoverImageUrl = c.CoverImageUrl,
                    IsPublished = c.IsPublished,
                    PostCount = c.Posts.Count(p => p.IsPublished && !p.IsDeleted)
                })
                .ToListAsync(cancellationToken);
        }, timeOutInSeconds: 300, cancellationToken);
    }

    public async Task<CourseDto?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default)
    {
        return await repository.Query()
            .Where(c => c.Slug == slug)
            .Select(c => new CourseDto
            {
                Id = c.Id,
                Title = c.Title,
                Slug = c.Slug,
                Description = c.Description,
                CoverImageUrl = c.CoverImageUrl,
                IsPublished = c.IsPublished,
                PostCount = c.Posts.Count(p => p.IsPublished && !p.IsDeleted)
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<long> CreateAsync(CreateCourseInputDto input, CancellationToken cancellationToken = default)
    {
        var entity = new Course
        {
            Title = input.Title,
            Slug = input.Slug,
            Description = input.Description,
            CoverImageUrl = input.CoverImageUrl,
            IsPublished = input.IsPublished
        };

        repository.Add(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        cache.Remove(CacheKeys.CoursesList);
        return entity.Id;
    }

    public async Task UpdateAsync(UpdateCourseInputDto input, CancellationToken cancellationToken = default)
    {
        var entity = await repository.FindByIdAsync(input.Id)
            ?? throw new KeyNotFoundException("دوره یافت نشد.");

        entity.Title = input.Title;
        entity.Slug = input.Slug;
        entity.Description = input.Description;
        entity.CoverImageUrl = input.CoverImageUrl;
        entity.IsPublished = input.IsPublished;

        repository.Update(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        cache.Remove(CacheKeys.CoursesList);
    }

    public async Task DeleteAsync(long id, CancellationToken cancellationToken = default)
    {
        var entity = await repository.FindByIdAsync(id);
        if (entity is null) return;

        repository.Delete(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        cache.Remove(CacheKeys.CoursesList);
    }
}
