namespace PersonalBlog.Domain.Commons.Base;

public interface ISoftDeletable
{
    bool IsDeleted { get; set; }
    long? DeletedBy { get; set; }
    DateTime? DeletedAt { get; set; }

    void MarkAsDeleted(long userId)
    {
        IsDeleted = true;
        DeletedAt = DateTime.Now;
        DeletedBy = userId;
    }
}
