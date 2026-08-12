using FluentValidation;
using PersonalBlog.Domain.Commons;
using PersonalBlog.Domain.Entities.Courses;
using PersonalBlog.Domain.Exceptions;

namespace AppServices.Admin.CoursePosts.Delete;

public class RemovePostFromCourseCommandHandler(
    IValidator<RemovePostFromCourseCommand> validator,
    ICoursePostRepository coursePostRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<RemovePostFromCourseCommand, int>
{
    public async Task<int> Invoke(
        RemovePostFromCourseCommand input,
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

        coursePostRepository.Delete(coursePost);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return coursePost.Id;
    }
}