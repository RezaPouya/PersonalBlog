using PersonalBlog.Domain.Entities.Courses;
using PersonalBlog.Domain.Entities.Courses.Dtos;

namespace AppServices.Site.Courses;

public class GetCoursesForSiteQueryHandler(ICourseRepository courseRepository)
    : IQueryHandler<GetCoursesForSiteQuery, List<CourseSiteListDto>>
{
    public async Task<List<CourseSiteListDto>> Handle(GetCoursesForSiteQuery input, CancellationToken cancellationToken)
    {
        return await courseRepository.GetPublishedListForSiteAsync(cancellationToken);
    }
}
