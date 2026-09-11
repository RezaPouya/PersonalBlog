namespace PersonalBlog.Domain.Commons.Base;

/// <summary>
/// کلاس پایه‌ی همه‌ی موجودیت‌ها. هم‌ساختار با EntityBase پروژه‌ی نمونه (Iau.Bazaar).
/// </summary>
public abstract class EntityBase : IEntityBase
{
    protected EntityBase()
    {
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
    }

    public long Id { get; set; }
    public long? CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public long? UpdatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; } = DateTime.Now;
}
