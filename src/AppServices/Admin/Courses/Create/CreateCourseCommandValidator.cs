using FluentValidation;

namespace AppServices.Admin.Courses.Create;

public class CreateCourseCommandValidator
    : AbstractValidator<CreateCourseCommand>
{
    public CreateCourseCommandValidator()
    {
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