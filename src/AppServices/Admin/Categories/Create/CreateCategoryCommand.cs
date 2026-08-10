using PersonalBlog.Domain.Commons;
using PersonalBlog.Domain.Entities.Categories;

namespace AppServices.Admin.Categories.Create;

public class CreateCategoryCommand(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
{
    public async Task<int> Invoke(CreateCategoryInputDto inputDto, CancellationToken cancellationToken)
    {
        inputDto.Sanitize()
    }
}

