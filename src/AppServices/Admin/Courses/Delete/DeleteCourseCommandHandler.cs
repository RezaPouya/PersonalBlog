using FluentValidation;
using PersonalBlog.Domain.Commons;
using PersonalBlog.Domain.Constants;
using PersonalBlog.Domain.Entities.Courses;
using PersonalBlog.Domain.Exceptions;

namespace AppServices.Admin.Courses.Delete;

public class DeleteCourseCommandHandler(
    IValidator<DeleteCourseCommand> validator,
    ILocalCacheManager localCacheManager,
    ICourseRepository courseRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteCourseCommand, int>
{
    public async Task<int> Invoke(
        DeleteCourseCommand input,
        CancellationToken cancellationToken)
    {
        var validationResult = validator.Validate(input);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var course = await courseRepository.FindByIdAsync(
            input.Id,
            cancellationToken);

        if (course is null)
            throw new BusinessException("دوره یافت نشد.");

        if (await courseRepository.HasAnyPostAsync(
                course.Id,
                cancellationToken))
        {
            throw new BusinessException(
                "این دوره دارای مطلب است و قابل حذف نیست. ابتدا مطالب دوره را حذف کنید.");
        }

        courseRepository.Delete(course);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        localCacheManager.Remove(CacheKeys.CoursesList);

        return course.Id;
    }
}