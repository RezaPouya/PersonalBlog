namespace PersonalBlog.Domain.Commons.Base;

public interface ISoftDelete
{
    bool IsDeleted { get; set; }
    DateTime? DeletedAt { get; set; }

    void MarkAsDeleted()
    {
        IsDeleted = true;
        DeletedAt = DateTime.Now;

    }
}
