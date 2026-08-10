using AppServices.Base;
using FluentValidation;
using PersonalBlog.Domain.Commons;
using PersonalBlog.Domain.Constants;
using PersonalBlog.Domain.Entities.Categories;
using PersonalBlog.Domain.Entities.Posts;
using PersonalBlog.Domain.Exceptions;

namespace AppServices.Admin.Categories.Create;

public class DeleteCategoryCommandHandler(IValidator<DeleteCategoryCommand> validator,
    ILocalCacheManager localCacheManager,
    IPostRepository postRepository,
    ICategoryRepository categoryRepository, IUnitOfWork unitOfWork) : ICommandHandler<DeleteCategoryCommand, int>
{

    public async Task<int> Invoke(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var validationResult = validator.Validate(request);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        Category? category = await categoryRepository.FindByIdAsync(request.Id, cancellationToken);

        if (category is null)
            throw new BusinessException("دسته بندی با این شناسه یافت نشد.");

        var hasAnyPost = await postRepository.DoesCategoryHaveAnyPost(request.Id, cancellationToken);

        if (hasAnyPost)
            throw new BusinessException("این دسته بندی دارای پست فعال است.");

        categoryRepository.Delete(category);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        localCacheManager.Remove(CacheKeys.CategoriesList);

        return category.Id;
    }
}

