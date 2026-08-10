using PersonalBlog.Domain.Commons.Base;
using PersonalBlog.Domain.Entities.Posts;

namespace PersonalBlog.Domain.Entities.Categories;

public class Category : EntityBase
{
    public Category()
    {
        this.TinyUrl = "ct_" + Guid.CreateVersion7().ToString("N").Substring(0, 4).ToLower();
    }

    /// <summary>
    /// 150
    /// </summary>
    public string Title { get; set; } = default!;

    /// <summary>
    /// 150
    /// </summary>
    public string Slug { get; set; } = default!;

    public string TinyUrl { get; private set; }

    /// <summary>
    /// 2048
    /// </summary>
    public string? Description { get; set; }

    public bool IsInEnglish { get; set; }

    public ICollection<Post> Posts { get; set; } = new List<Post>();
}


public class CategoryConstantsLength
{
    public const int Title = 150;
}