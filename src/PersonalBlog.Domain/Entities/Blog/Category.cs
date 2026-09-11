using PersonalBlog.Domain.Commons.Base;

namespace PersonalBlog.Domain.Entities.Blog;

public class Category : EntityBase
{
    public string Title { get; set; } = default!;
    public string Slug { get; set; } = default!;
    public string? Description { get; set; }

    public ICollection<Post> Posts { get; set; } = new List<Post>();
}
