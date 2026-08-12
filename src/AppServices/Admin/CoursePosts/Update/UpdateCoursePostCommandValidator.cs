using FluentValidation;

namespace AppServices.Admin.CoursePosts.Update;

public class UpdateCoursePostCommandValidator
    : AbstractValidator<UpdateCoursePostCommand>
{
    public UpdateCoursePostCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);

        RuleFor(x => x.Title)
            .MaximumLength(200);

        RuleFor(x => x.Description)
            .MaximumLength(2048);

        RuleFor(x => x.CoverImageUrl)
            .MaximumLength(2048);

        RuleFor(x => x.OrderInCourse)
            .GreaterThan(0);
    }
}