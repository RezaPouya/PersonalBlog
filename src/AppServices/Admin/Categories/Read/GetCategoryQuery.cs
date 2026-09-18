using PersonalBlog.Domain.Entities.Categories.Dtos;

namespace AppServices.Admin.Categories.Read
{
    public class GetCategoryQuery : IQuery<CategoryDot>
    {
        public int Id { get; set; }
    }
}
