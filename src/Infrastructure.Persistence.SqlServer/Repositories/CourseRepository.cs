using PersonalBlog.Domain.Entities.Courses;
using PersonalBlog.Domain.Entities.Courses.Dtos;
using PersonalBlog.Utilities.Dtos;
using Utilities.Dtos;

namespace Infrastructure.Persistence.SqlServer.Repositories;

public class CourseRepository(AppDbContext dbContext)
    : RepositoryBase<Course>(dbContext), ICourseRepository
{
    public async Task<bool> IsExistsByTitleAsync(
        string title,
        int? id,
        CancellationToken cancellationToken)
    {
        var query = DbContext.Courses
            .AsNoTracking()
            .Where(x => x.Title == title);

        if (id.HasValue)
            query = query.Where(x => x.Id != id.Value);

        return await query.AnyAsync(cancellationToken);
    }

    public async Task<bool> IsExistsBySlugAsync(
        string slug,
        int? id,
        CancellationToken cancellationToken)
    {
        var query = DbContext.Courses
            .AsNoTracking()
            .Where(x => x.Slug == slug);

        if (id.HasValue)
            query = query.Where(x => x.Id != id.Value);

        return await query.AnyAsync(cancellationToken);
    }

    public async Task<bool> HasAnyPostAsync(
        int courseId,
        CancellationToken cancellationToken)
    {
        return await DbContext.CoursesPosts
            .AsNoTracking()
            .AnyAsync(
                x => x.CourseId == courseId,
                cancellationToken);
    }

    public async Task<GridDataSourceResult<CourseGridDto>> GetGridAsync(
        GridDataSourceRequest request,
        CancellationToken cancellationToken)
    {
        var query = DbContext.Courses
            .AsNoTracking()
            .Select(x => new CourseGridDto
            {
                Id = x.Id,
                Title = x.Title,
                Slug = x.Slug,
                TinyUrl = x.TinyUrl,
                Description = x.Description,
                CoverImageUrl = x.CoverImageUrl,
                IsPublished = x.IsPublished,
                IsInEnglish = x.IsInEnglish,
                OrderInCourses = x.OrderInCourses,

                PostsCount = x.CoursePosts.Count(),

                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt
            });

        return await query.ToDataSourceResult(
            request,
            cancellationToken);
    }

    public async Task<List<IdTitleDto<int>>> GetListForLookupAsync(
        CancellationToken cancellationToken)
    {
        return await DbContext.Courses
            .AsNoTracking()
            .Where(x => x.IsPublished)
            .OrderBy(x => x.OrderInCourses)
            .ThenBy(x => x.Title)
            .Select(x => new IdTitleDto<int>
            {
                Id = x.Id,
                Title = x.Title
            })
            .ToListAsync(cancellationToken);
    }
}