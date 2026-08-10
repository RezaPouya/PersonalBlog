namespace PersonalBlog.Domain.Commons.Base;

/// <summary>
/// کلاس پایه‌ی همه‌ی موجودیت‌ها. هم‌ساختار با EntityBase پروژه‌ی نمونه (PersonalBlog).
/// </summary>
public abstract class EntityBase : IEntityBase
{
    protected EntityBase()
    {
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
    }

    public int Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}
