using Infrastructure.Persistance.SqlServer.Converters;
using Infrastructure.Persistance.SqlServer.DbExtensions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalBlog.Domain.Entities;

namespace Infrastructure.Persistance.SqlServer.Configurations;

public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.ToTable("Courses");
        builder.ConfigureEntityBase<Course>();
        builder.Property(c => c.Title).HasMaxLength(200).IsRequired();
        builder.Property(c => c.Description).HasMaxLength(2048);
        builder.Property(c => c.CoverImageUrl).HasMaxLength(2048);
        builder.Property(c => c.TinyUrl).HasMaxLength(10).IsRequired();
        builder.Property(c => c.Slug).HasMaxLength(200).IsRequired();

        builder.Property(e => e.RelatedCourses).HasConversion(EfCoreValueConverter.CreateIntListValueConverter());

        builder.HasIndex(c => c.Slug).IsUnique();
        builder.HasIndex(c => c.TinyUrl).IsUnique();
    }
}
