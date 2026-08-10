using AppServices.Base;
using FluentValidation;
using PersonalBlog.Domain.Commons;
using PersonalBlog.Domain.Constants;
using PersonalBlog.Domain.Entities.Categories;
using PersonalBlog.Domain.Exceptions;

namespace AppServices.Admin.Categories.Create;

public class UpdateCategoryCommandHandler(IValidator<UpdateCategoryCommand> validator,
    ILocalCacheManager localCacheManager,
    ICategoryRepository categoryRepository, IUnitOfWork unitOfWork) : ICommandHandler<UpdateCategoryCommand, int>
{

    public async Task<int> Invoke(UpdateCategoryCommand input, CancellationToken cancellationToken)
    {
        var validationResult = validator.Validate(input);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        input.Sanitize();

        Category? category = await categoryRepository.FindByIdAsync(input.Id, cancellationToken);

        if (category is null)
            throw new BusinessException("دسته بندی با این شناسه یافت نشد.");

        category.Title = input.Title;
        category.Slug = input.Slug;
        category.Description = input.Description;
        category.IsInEnglish = input.IsInEnglish;

        await unitOfWork.SaveChangesAsync(cancellationToken);

        localCacheManager.Remove(CacheKeys.CategoriesList);

        return category.Id;
    }
}

