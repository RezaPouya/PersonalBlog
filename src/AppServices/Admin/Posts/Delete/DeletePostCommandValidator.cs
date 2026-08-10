using FluentValidation;

namespace AppServices.Admin.Posts.Delete;

public class DeletePostCommandValidator : AbstractValidator<DeletePostCommand>
{
    public DeletePostCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}
