using FluentValidation;
using PersonalBlog.Domain.Commons;
using PersonalBlog.Domain.Constants;
using PersonalBlog.Domain.Entities.Courses;
using PersonalBlog.Domain.Exceptions;

namespace AppServices.Admin.Courses.Create;

public class CreateCourseCommandHandler(
    IValidator<CreateCourseCommand> validator,
    ILocalCacheManager localCacheManager,
    ICourseRepository courseRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateCourseCommand, int>
{
    public async Task<int> Invoke(
        CreateCourseCommand input,
        CancellationToken cancellationToken)
    {
        var validationResult = validator.Validate(input);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        input.Sanitize();

        if (await courseRepository.IsExistsByTitleAsync(
                input.Title,
                null,
                cancellationToken))
        {
            throw new BusinessException(
                "دوره‌ای با این عنوان وجود دارد.");
        }

        if (await courseRepository.IsExistsBySlugAsync(
                input.Slug,
                null,
                cancellationToken))
        {
            throw new BusinessException(
                "دوره‌ای با این اسلاگ وجود دارد.");
        }

        var course = new Course
        {
            Title = input.Title,
            Slug = input.Slug,
            Description = input.Description,
            CoverImageUrl = input.CoverImageUrl,
            IsPublished = input.IsPublished,
            IsInEnglish = input.IsInEnglish,
            OrderInCourses = input.OrderInCourses
        };

        courseRepository.Create(course);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        localCacheManager.Remove(CacheKeys.CoursesList);

        return course.Id;
    }
}