using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalBlog.Domain.Entities.Blog;

namespace PersonalBlog.Infrastructure.Configurations;

public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.ToTable("Posts");

        builder.Property(p => p.Title).HasMaxLength(250).IsRequired();
        builder.Property(p => p.Slug).HasMaxLength(250).IsRequired();
        builder.Property(p => p.Summary).HasMaxLength(500).IsRequired();
        builder.Property(p => p.Content).IsRequired();
        builder.Property(p => p.MetaTitle).HasMaxLength(250);
        builder.Property(p => p.MetaDescription).HasMaxLength(500);

        builder.HasIndex(p => p.Slug).IsUnique();
        builder.HasIndex(p => p.IsPublished);
        builder.HasQueryFilter(p => !p.IsDeleted);

        builder.HasOne(p => p.Category)
            .WithMany(c => c.Posts)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.Course)
            .WithMany(c => c.Posts)
            .HasForeignKey(p => p.CourseId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
