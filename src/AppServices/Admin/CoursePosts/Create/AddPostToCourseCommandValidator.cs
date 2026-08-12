using FluentValidation;

namespace AppServices.Admin.CoursePosts.Create;

public class AddPostToCourseCommandValidator
    : AbstractValidator<AddPostToCourseCommand>
{
    public AddPostToCourseCommandValidator()
    {
        RuleFor(x => x.CourseId)
            .GreaterThan(0);

        RuleFor(x => x.PostId)
            .GreaterThan(0);

        RuleFor(x => x.Title)
            .MaximumLength(200);

        RuleFor(x => x.Description)
            .MaximumLength(2048);

        RuleFor(x => x.CoverImageUrl)
            .MaximumLength(2048);

        RuleFor(x => x.OrderInCourse)
            .GreaterThan(0)
            .When(x => x.OrderInCourse.HasValue);
    }
}