using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalBlog.Domain.Commons.Base;

namespace Infrastructure.Persistence.SqlServer.DbExtensions;

public static class EntityBaseConfiguration
{
    public static void ConfigureEntityBase<TEntity>(this EntityTypeBuilder<TEntity> builder) where TEntity : EntityBase
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(p => p.CreatedAt).IsRequired();
        builder.Property(p => p.UpdatedAt).IsRequired();
    }

    public static void ConfigureSoftwareDeletable<TEntity>(this EntityTypeBuilder<TEntity> builder) where TEntity : class, ISoftDelete
    {
        builder.Property(p => p.DeletedAt).IsRequired(false);
    }
}