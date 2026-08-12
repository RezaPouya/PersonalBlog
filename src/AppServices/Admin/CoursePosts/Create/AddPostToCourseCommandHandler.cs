using FluentValidation;
using PersonalBlog.Domain.Commons;
using PersonalBlog.Domain.Entities.Courses;
using PersonalBlog.Domain.Entities.Posts;
using PersonalBlog.Domain.Exceptions;

namespace AppServices.Admin.CoursePosts.Create;

public class AddPostToCourseCommandHandler(
    IValidator<AddPostToCourseCommand> validator,
    ICourseRepository courseRepository,
    ICoursePostRepository coursePostRepository,
    IPostRepository postRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<AddPostToCourseCommand, int>
{
    public async Task<int> Invoke(
        AddPostToCourseCommand input,
        CancellationToken cancellationToken)
    {
        var validationResult = validator.Validate(input);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var course = await courseRepository.FindByIdAsync(
            input.CourseId,
            cancellationToken);

        if (course is null)
            throw new BusinessException("دوره یافت نشد.");

        var post = await postRepository.FindByIdAsync(
            input.PostId,
            cancellationToken);

        if (post is null)
            throw new BusinessException("مطلب یافت نشد.");

        if (await coursePostRepository.IsExistsAsync(
                input.CourseId,
                input.PostId,
                null,
                cancellationToken))
        {
            throw new BusinessException(
                "این مطلب قبلاً در این دوره قرار گرفته است.");
        }

        var order = input.OrderInCourse
                    ?? await coursePostRepository.GetNextOrderAsync(
                        input.CourseId,
                        cancellationToken);

        var coursePost = new CoursePost
        {
            CourseId = input.CourseId,
            PostId = input.PostId,

            Title = input.Title,
            Description = input.Description,
            CoverImageUrl = input.CoverImageUrl,

            IsPublished = input.IsPublished,

            OrderInCourse = order
        };

        coursePostRepository.Create(coursePost);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return coursePost.Id;
    }
}