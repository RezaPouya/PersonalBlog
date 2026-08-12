using PersonalBlog.Domain.Entities.Courses;
using PersonalBlog.Domain.Entities.Courses.Dtos;
using PersonalBlog.Utilities.Dtos;

namespace Infrastructure.Persistence.SqlServer.Repositories;

public class CoursePostRepository(AppDbContext dbContext) : RepositoryBase<CoursePost>(dbContext), ICoursePostRepository
{
    public async Task<bool> IsExistsAsync(
        int courseId,
        int postId,
        int? id,
        CancellationToken cancellationToken)
    {
        var query = DbContext.CoursesPosts
            .AsNoTracking()
            .Where(x =>
                x.CourseId == courseId &&
                x.PostId == postId);

        if (id.HasValue)
            query = query.Where(x => x.Id != id.Value);

        return await query.AnyAsync(cancellationToken);
    }

    public async Task<int> GetNextOrderAsync(
        int courseId,
        CancellationToken cancellationToken)
    {
        var maxOrder = await DbContext.CoursesPosts
            .AsNoTracking()
            .Where(x => x.CourseId == courseId)
            .Select(x => (int?)x.OrderInCourse)
            .MaxAsync(cancellationToken);

        return (maxOrder ?? 0) + 1;
    }

    public async Task<GridDataSourceResult<CoursePostGridDto>> GetGridAsync(
        int courseId,
        GridDataSourceRequest request,
        CancellationToken cancellationToken)
    {
        var query = DbContext.CoursesPosts
            .AsNoTracking()
            .Where(x => x.CourseId == courseId)
            .OrderBy(x => x.OrderInCourse)
            .Select(x => new CoursePostGridDto
            {
                Id = x.Id,
                CourseId = x.CourseId,
                PostId = x.PostId,

                PostTitle = x.Post.Title,

                Title = x.Title,
                Description = x.Description,
                CoverImageUrl = x.CoverImageUrl,

                IsPublished = x.IsPublished,

                OrderInCourse = x.OrderInCourse,

                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt
            });

        return await query.ToDataSourceResult(
            request,
            cancellationToken);
    }

    public async Task<bool> IsPostAlreadyInCourseAsync(int courseId, int postId, int? id, CancellationToken cancellationToken)
    {
        var query = DbContext.CoursesPosts
            .AsNoTracking()
            .Where(x => x.CourseId == courseId && x.PostId == postId);

        if (id.HasValue && id.Value > 0)
            query = query.Where(p => p.Id != id);

        return await query.AnyAsync(cancellationToken);
    }
}