using PersonalBlog.Domain.Commons.Base;

namespace PersonalBlog.Domain.Entities;

/// <summary>جدول واسط چند-به-چند بین Post و Tag.</summary>
public class PostTag : EntityBase
{
    public int PostId { get; set; }
    public Post Post { get; set; } = default!;

    public int TagId { get; set; }
    public Tag Tag { get; set; } = default!;
}
