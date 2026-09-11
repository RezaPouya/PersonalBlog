using Microsoft.EntityFrameworkCore;
using PersonalBlog.AppServices.Dtos;
using PersonalBlog.Domain.Commons;
using PersonalBlog.Domain.Constants;
using PersonalBlog.Domain.Entities.Blog;

namespace PersonalBlog.AppServices.Services.Blog.Imps;

public class ProjectService(
    IRepository<Project> repository,
    IUnitOfWork unitOfWork,
    ILocalCacheManager cache) : IProjectService
{
    public async Task<List<ProjectDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await cache.GetOrCreateAsync(CacheKeys.ProjectsList, async () =>
        {
            return await repository.Query()
                .OrderByDescending(p => p.IsFeatured)
                .ThenBy(p => p.DisplayOrder)
                .Select(p => new ProjectDto
                {
                    Id = p.Id,
                    Title = p.Title,
                    Slug = p.Slug,
                    Description = p.Description,
                    ImageUrl = p.ImageUrl,
                    LiveUrl = p.LiveUrl,
                    RepoUrl = p.RepoUrl,
                    Technologies = (p.TechnologiesCsv ?? "")
                        .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                        .ToList(),
                    IsFeatured = p.IsFeatured
                })
                .ToListAsync(cancellationToken);
        }, timeOutInSeconds: 300, cancellationToken);
    }

    public async Task<long> CreateAsync(CreateProjectInputDto input, CancellationToken cancellationToken = default)
    {
        var entity = new Project
        {
            Title = input.Title,
            Slug = input.Slug,
            Description = input.Description,
            ImageUrl = input.ImageUrl,
            LiveUrl = input.LiveUrl,
            RepoUrl = input.RepoUrl,
            TechnologiesCsv = input.TechnologiesCsv,
            DisplayOrder = input.DisplayOrder,
            IsFeatured = input.IsFeatured
        };

        repository.Add(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        cache.Remove(CacheKeys.ProjectsList);
        return entity.Id;
    }

    public async Task UpdateAsync(UpdateProjectInputDto input, CancellationToken cancellationToken = default)
    {
        var entity = await repository.FindByIdAsync(input.Id)
            ?? throw new KeyNotFoundException("پروژه یافت نشد.");

        entity.Title = input.Title;
        entity.Slug = input.Slug;
        entity.Description = input.Description;
        entity.ImageUrl = input.ImageUrl;
        entity.LiveUrl = input.LiveUrl;
        entity.RepoUrl = input.RepoUrl;
        entity.TechnologiesCsv = input.TechnologiesCsv;
        entity.DisplayOrder = input.DisplayOrder;
        entity.IsFeatured = input.IsFeatured;

        repository.Update(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        cache.Remove(CacheKeys.ProjectsList);
    }

    public async Task DeleteAsync(long id, CancellationToken cancellationToken = default)
    {
        var entity = await repository.FindByIdAsync(id);
        if (entity is null) return;

        repository.Delete(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        cache.Remove(CacheKeys.ProjectsList);
    }
}
