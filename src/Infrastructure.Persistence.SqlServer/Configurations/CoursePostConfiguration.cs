using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalBlog.Domain.Entities.Courses;

namespace Infrastructure.Persistence.SqlServer.Configurations;

public class CoursePostConfiguration : IEntityTypeConfiguration<CoursePost>
{
    public void Configure(EntityTypeBuilder<CoursePost> builder)
    {
        builder.ToTable("CoursesPosts");
        builder.ConfigureEntityBase<CoursePost>();
        builder.Property(c => c.Title).HasMaxLength(200);
        builder.Property(c => c.Description).HasMaxLength(2048);
        builder.Property(c => c.CoverImageUrl).HasMaxLength(2048);


        builder.HasOne(p => p.Course)
            .WithMany(c => c.CoursePosts)
            .HasForeignKey(p => p.CourseId)
            .OnDelete(DeleteBehavior.Restrict);


        builder.HasOne(p => p.Post)
            .WithMany(c => c.CoursePosts)
            .HasForeignKey(p => p.PostId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new
        {
            x.CourseId,
            x.PostId
        }).IsUnique();
    }
}
