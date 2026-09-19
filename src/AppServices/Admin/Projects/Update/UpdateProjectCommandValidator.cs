using FluentValidation;

namespace AppServices.Admin.Projects.Update;

public class UpdateProjectCommandValidator : AbstractValidator<UpdateProjectCommand>
{
    public UpdateProjectCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).WithMessage("شناسه اجباری است.");
        RuleFor(x => x.Title).NotEmpty().WithMessage("عنوان نباید خالی باشد.").MaximumLength(200);
        RuleFor(x => x.Slug).NotEmpty().WithMessage("اسلاگ نباید خالی باشد.").MaximumLength(200);
        RuleFor(x => x.Description).NotEmpty().WithMessage("توضیحات نباید خالی باشد.").MaximumLength(4000);
    }
}
