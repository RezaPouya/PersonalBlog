using PersonalBlog.Domain.Entities.Categories;
using PersonalBlog.Domain.Entities.Categories.Dtos;
using PersonalBlog.Domain.Exceptions;

namespace AppServices.Admin.Categories.Read;

public class GetCategoryQueryHandler(ICategoryRepository categoryRepository) : IQueryHandler<GetCategoryQuery, CategoryDot>
{
    public async Task<CategoryDot> Invoke(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        CategoryDot? record = await categoryRepository.GetCategoryInfoByIdAsync(request.Id, cancellationToken)
            ?? throw new BusinessException("رکوردی با این شناسه یافت نشد");

        return record;
    }
}