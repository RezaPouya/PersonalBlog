using PersonalBlog.Domain.Entities.Categories;
using PersonalBlog.Domain.Entities.Categories.Dtos;

namespace AppServices.Site.Categories;

public class GetCategoryBySlugQueryHandler(ICategoryRepository categoryRepository)
    : IQueryHandler<GetCategoryBySlugQuery, CategoryDot?>
{
    public async Task<CategoryDot?> Handle(GetCategoryBySlugQuery input, CancellationToken cancellationToken)
    {
        return await categoryRepository.GetBySlugAsync(input.Slug, cancellationToken);
    }
}
