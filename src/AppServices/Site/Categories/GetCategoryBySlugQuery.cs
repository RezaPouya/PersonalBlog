using PersonalBlog.Domain.Entities.Categories.Dtos;

namespace AppServices.Site.Categories;

public class GetCategoryBySlugQuery : IQuery<CategoryDot?>
{
    public string Slug { get; set; } = default!;
}
