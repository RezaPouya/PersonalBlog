using FluentValidation;

public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).WithMessage("شناسه اجباری است.");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("عنوان نباید خالی باشد.")
            .MaximumLength(150).WithMessage("عنوان حداکثر 150 کاراکتر مجاز است.");

        RuleFor(x => x.Slug)
            .NotEmpty().WithMessage("اسلاگ نباید خالی باشد.")
            .MaximumLength(150).WithMessage("اسلاگ حداکثر 150 کاراکتر مجاز است.");

        RuleFor(x => x.Description)
            .MaximumLength(2048).WithMessage("توضیحات حداکثر 2049 کاراکتر مجاز است.")
            .When(x => x.Description != null);
    }
}