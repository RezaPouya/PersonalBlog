using PersonalBlog.Domain.Commons.Base;

namespace PersonalBlog.Domain.Entities;

public class Tag : EntityBase
{
    public string Title { get; set; } = default!;
    public string Slug { get; set; } = default!;

    public ICollection<PostTag> PostTags { get; set; } = new List<PostTag>();
}
