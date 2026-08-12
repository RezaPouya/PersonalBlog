using FluentValidation;
using PersonalBlog.Domain.Commons;
using PersonalBlog.Domain.Constants;
using PersonalBlog.Domain.Entities.Courses;
using PersonalBlog.Domain.Exceptions;

namespace AppServices.Admin.Courses.Update;

public class UpdateCourseCommandHandler(
    IValidator<UpdateCourseCommand> validator,
    ILocalCacheManager localCacheManager,
    ICourseRepository courseRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateCourseCommand, int>
{
    public async Task<int> Invoke(
        UpdateCourseCommand input,
        CancellationToken cancellationToken)
    {
        var validationResult = validator.Validate(input);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        input.Sanitize();

        var course = await courseRepository.FindByIdAsync(
            input.Id,
            cancellationToken);

        if (course is null)
            throw new BusinessException("دوره یافت نشد.");

        if (await courseRepository.IsExistsByTitleAsync(
                input.Title,
                input.Id,
                cancellationToken))
        {
            throw new BusinessException(
                "دوره دیگری با این عنوان وجود دارد.");
        }

        if (await courseRepository.IsExistsBySlugAsync(
                input.Slug,
                input.Id,
                cancellationToken))
        {
            throw new BusinessException(
                "دوره دیگری با این اسلاگ وجود دارد.");
        }

        course.Title = input.Title;
        course.Slug = input.Slug;
        course.Description = input.Description;
        course.CoverImageUrl = input.CoverImageUrl;
        course.IsPublished = input.IsPublished;
        course.IsInEnglish = input.IsInEnglish;
        course.OrderInCourses = input.OrderInCourses;

        courseRepository.SetUpdatedAt(course);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        localCacheManager.Remove(CacheKeys.CoursesList);

        return course.Id;
    }
}