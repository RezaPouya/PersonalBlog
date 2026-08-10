using Infrastructure.Persistance.SqlServer.DbExtensions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalBlog.Domain.Entities.Categories;

namespace Infrastructure.Persistance.SqlServer.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories");
        builder.ConfigureEntityBase<Category>();
        builder.Property(c => c.Title).HasMaxLength(150).IsRequired();
        builder.Property(c => c.Description).HasMaxLength(2048).IsRequired();
        builder.Property(c => c.Slug).HasMaxLength(150).IsRequired();
        builder.Property(c => c.TinyUrl).HasMaxLength(10).IsRequired();
        builder.HasIndex(c => c.Slug).IsUnique();
        builder.HasIndex(c => c.TinyUrl).IsUnique();
    }
}
