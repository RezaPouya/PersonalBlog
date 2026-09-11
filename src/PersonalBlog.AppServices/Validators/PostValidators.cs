using FluentValidation;
using PersonalBlog.AppServices.Dtos;

namespace PersonalBlog.AppServices.Validators;

public class CreatePostInputDtoValidator : AbstractValidator<CreatePostInputDto>
{
    public CreatePostInputDtoValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Slug).NotEmpty().MaximumLength(200)
            .Matches("^[a-z0-9-]+$").WithMessage("اسلاگ فقط می‌تواند شامل حروف انگلیسی کوچک، عدد و خط تیره باشد.");
        RuleFor(x => x.Summary).NotEmpty().MaximumLength(500);
        RuleFor(x => x.Content).NotEmpty();
        RuleFor(x => x.CategoryId).GreaterThan(0);
    }
}

public class UpdatePostInputDtoValidator : AbstractValidator<UpdatePostInputDto>
{
    public UpdatePostInputDtoValidator()
    {
        Include(new CreatePostInputDtoValidator());
        RuleFor(x => x.Id).GreaterThan(0);
    }
}
