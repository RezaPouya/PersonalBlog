using FluentValidation;
using PersonalBlog.Domain.Commons;
using PersonalBlog.Domain.Entities.Courses;
using PersonalBlog.Domain.Exceptions;

namespace AppServices.Admin.CoursePosts.Update;

public class UpdateCoursePostCommandHandler(
    IValidator<UpdateCoursePostCommand> validator,
    ICoursePostRepository coursePostRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateCoursePostCommand, int>
{
    public async Task<int> Invoke(
        UpdateCoursePostCommand input,
        CancellationToken cancellationToken)
    {
        var validationResult = validator.Validate(input);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var coursePost = await coursePostRepository.FindByIdAsync(
            input.Id,
            cancellationToken);

        if (coursePost is null)
            throw new BusinessException(
                "مطلب دوره یافت نشد.");

        coursePost.Title = input.Title;
        coursePost.Description = input.Description;
        coursePost.CoverImageUrl = input.CoverImageUrl;
        coursePost.IsPublished = input.IsPublished;
        coursePost.OrderInCourse = input.OrderInCourse;

        coursePostRepository.SetUpdatedAt(coursePost);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return coursePost.Id;
    }
}