global using PersonalBlog.Domain.Commons.Base;
using PersonalBlog.Domain.Entities.Posts.Entities;

namespace PersonalBlog.Domain.Entities.Tags;

public class Tag : EntityBase
{
    public string Title { get; set; } = default!;
    public ICollection<PostTag> PostTags { get; set; } = new List<PostTag>();
}
