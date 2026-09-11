using FluentValidation;

namespace AppServices.Admin.Courses.Delete;

public class DeleteCourseCommandValidator : AbstractValidator<DeleteCourseCommand>
{
    public DeleteCourseCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}