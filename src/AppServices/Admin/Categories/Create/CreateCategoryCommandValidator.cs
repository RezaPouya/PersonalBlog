using FluentValidation;

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
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