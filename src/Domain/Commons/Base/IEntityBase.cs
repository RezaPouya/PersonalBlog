namespace PersonalBlog.Domain.Commons.Base;

public interface IEntityBase
{
    DateTime CreatedAt { get; set; }
    DateTime UpdatedAt { get; set; }
}
