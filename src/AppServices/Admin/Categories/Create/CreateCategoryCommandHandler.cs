using AppServices.Base;
using FluentValidation;
using PersonalBlog.Domain.Commons;
using PersonalBlog.Domain.Constants;
using PersonalBlog.Domain.Entities.Categories;
using PersonalBlog.Domain.Exceptions;

namespace AppServices.Admin.Categories.Create;

public class CreateCategoryCommandHandler(IValidator<CreateCategoryCommand> validator,
    ILocalCacheManager localCacheManager,
    ICategoryRepository categoryRepository, IUnitOfWork unitOfWork) : ICommandHandler<CreateCategoryCommand, int>
{

    public async Task<int> Invoke(CreateCategoryCommand input, CancellationToken cancellationToken)
    {
        var validationResult = validator.Validate(input);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        input.Sanitize();

        bool isDuplicated = await categoryRepository.IsExistsByTitleAsync(input.Title, cancellationToken);

        if (isDuplicated)
            throw new BusinessException("دسته بندی با همین نام وجود دارد.");

        var category = new Category()
        {
            Description = input.Description,
            Slug = input.Slug,
            Title = input.Title,
            IsInEnglish = input.IsInEnglish
        };

        categoryRepository.Create(category);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        localCacheManager.Remove(CacheKeys.CategoriesList);

        return category.Id;
    }
}

