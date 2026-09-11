namespace PersonalBlog.Domain.Commons.Base;

public interface IEntityBase
{
    long? CreatedBy { get; set; }
    DateTime CreatedAt { get; set; }
    long? UpdatedBy { get; set; }
    DateTime? UpdatedAt { get; set; }

    void SetCreatedBy(long userId)
    {
        CreatedAt = DateTime.Now;
        CreatedBy = userId;
    }

    void SetUpdatedBy(long userId)
    {
        UpdatedAt = DateTime.Now;
        UpdatedBy = userId;
    }
}
