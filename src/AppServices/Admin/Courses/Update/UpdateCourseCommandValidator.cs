using FluentValidation;

namespace AppServices.Admin.Courses.Update;

public class UpdateCourseCommandValidator
    : AbstractValidator<UpdateCourseCommand>
{
    public UpdateCourseCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);

        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Slug)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Description)
            .MaximumLength(2048);

        RuleFor(x => x.CoverImageUrl)
            .MaximumLength(2048);

        RuleFor(x => x.OrderInCourses)
            .GreaterThanOrEqualTo(0);
    }
}