using FluentValidation;

namespace AppServices.Admin.CoursePosts.Delete;

public class RemovePostFromCourseCommandValidator
    : AbstractValidator<RemovePostFromCourseCommand>
{
    public RemovePostFromCourseCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);
    }
}