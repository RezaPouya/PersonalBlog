using PersonalBlog.Domain.Commons.Base;

namespace PersonalBlog.Domain.Entities;

public class Category : EntityBase
{
    public Category()
    {
        this.TinyUrl = "ct_" + Guid.CreateVersion7().ToString("N").Substring(0, 4).ToLower();
    }

    public string Title { get; set; } = default!;
    public string Slug { get; set; } = default!;
    public string TinyUrl { get; set; }
    public string? Description { get; set; }

    public bool IsInEnglish { get; set; }

    public ICollection<Post> Posts { get; set; } = new List<Post>();
}
