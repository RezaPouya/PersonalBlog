using PersonalBlog.Domain.Entities.Tags;

namespace PersonalBlog.Domain.Entities.Posts.Entities;

/// <summary>جدول واسط چند-به-چند بین Post و Tag.</summary>
public class PostTag : EntityBase
{
    public int PostId { get; set; }
    public Post Post { get; set; } = default!;

    public int TagId { get; set; }
    public Tag Tag { get; set; } = default!;
}
