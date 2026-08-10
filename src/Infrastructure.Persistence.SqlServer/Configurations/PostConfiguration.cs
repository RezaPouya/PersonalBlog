using Infrastructure.Persistence.SqlServer.Converters;
using Infrastructure.Persistence.SqlServer.DbExtensions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalBlog.Domain.Entities.Posts;

namespace Infrastructure.Persistence.SqlServer.Configurations;

public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.ToTable("Posts");
        builder.ConfigureEntityBase<Post>();

        builder.Property(p => p.Title).HasMaxLength(250).IsRequired();

        builder.Property(p => p.Slug).HasMaxLength(250).IsRequired();

        builder.Property(c => c.TinyUrl).HasMaxLength(10).IsRequired();

        builder.Property(p => p.Summary).HasMaxLength(500).IsRequired();

        builder.Property(c => c.CoverImageUrl).HasMaxLength(2048);

        builder.Property(p => p.Content).IsRequired();

        builder.Property(p => p.MetaTitle).HasMaxLength(250);

        builder.Property(p => p.MetaDescription).HasMaxLength(500);

        builder.Property(c => c.OgImageUrl).HasMaxLength(2048);

        builder.Property(c => c.RelatedPosts).HasMaxLength(2048)
        .HasConversion(EfCoreValueConverter.CreateIntListValueConverter()); ;

        builder.HasIndex(p => p.Slug).IsUnique();
        builder.HasIndex(p => p.TinyUrl).IsUnique();

        builder.HasIndex(p => p.IsPublished);
        builder.HasQueryFilter(p => !p.IsDeleted);

        builder.HasOne(p => p.Category)
            .WithMany(c => c.Posts)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);


    }
}
