using FluentValidation;

namespace AppServices.Admin.Projects.Create;

public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
{
    public CreateProjectCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty().WithMessage("عنوان نباید خالی باشد.").MaximumLength(200);
        RuleFor(x => x.Slug).NotEmpty().WithMessage("اسلاگ نباید خالی باشد.").MaximumLength(200);
        RuleFor(x => x.Description).NotEmpty().WithMessage("توضیحات نباید خالی باشد.").MaximumLength(4000);
    }
}
