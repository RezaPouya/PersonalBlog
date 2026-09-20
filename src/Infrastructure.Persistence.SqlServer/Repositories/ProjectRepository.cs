using PersonalBlog.Domain.Entities.Projects;
using PersonalBlog.Domain.Entities.Projects.Dtos;
using Utilities.Dtos;

namespace Infrastructure.Persistence.SqlServer.Repositories;

public class ProjectRepository(AppDbContext dbContext) : RepositoryBase<Project>(dbContext), IProjectRepository
{
    public async Task<GridDataSourceResult<ProjectGridDto>> GetGridAsync(GridDataSourceRequest request, CancellationToken cancellationToken)
    {
        var query = base.DbContext.Projects.AsNoTracking()
            .Select(p => new ProjectGridDto
            {
                Id = p.Id,
                Title = p.Title,
                Slug = p.Slug,
                ImageUrl = p.ImageUrl,
                IsFeatured = p.IsFeatured,
                IsInEnglish = p.IsInEnglish,
                OrderInProjects = p.OrderInProjects,
                CreatedAt = p.CreatedAt
            });

        return await query.ToDataSourceResult(request, cancellationToken);
    }

    public async Task<ProjectDto?> GetInfoByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await base.DbContext.Projects.AsNoTracking()
            .Where(p => p.Id == id)
            .Select(p => new ProjectDto
            {
                Id = p.Id,
                Title = p.Title,
                Slug = p.Slug,
                Description = p.Description,
                ImageUrl = p.ImageUrl,
                LiveUrl = p.LiveUrl,
                RepoUrl = p.RepoUrl,
                TechnologiesCsv = p.TechnologiesCsv,
                OrderInProjects = p.OrderInProjects,
                IsFeatured = p.IsFeatured,
                IsInEnglish = p.IsInEnglish
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<bool> IsExistsBySlugAsync(string slug, int? id, CancellationToken cancellationToken)
    {
        return await base.DbContext.Projects.AsNoTracking()
            .Where(p => p.Slug == slug && (id == null || p.Id != id))
            .AnyAsync(cancellationToken);
    }

    public async Task<List<ProjectSiteDto>> GetListForSiteAsync(CancellationToken cancellationToken)
    {
        return await base.DbContext.Projects.AsNoTracking()
            .OrderByDescending(p => p.IsFeatured)
            .ThenBy(p => p.OrderInProjects)
            .ThenByDescending(p => p.CreatedAt)
            .Select(p => new ProjectSiteDto
            {
                Title = p.Title,
                Slug = p.Slug,
                Description = p.Description,
                ImageUrl = p.ImageUrl,
                LiveUrl = p.LiveUrl,
                RepoUrl = p.RepoUrl,
                TechnologiesCsv = p.TechnologiesCsv,
                IsFeatured = p.IsFeatured
            })
            .ToListAsync(cancellationToken);
    }
}
