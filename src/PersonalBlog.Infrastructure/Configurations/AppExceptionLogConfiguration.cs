using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalBlog.Domain.Exceptions;

namespace PersonalBlog.Infrastructure.Configurations;

public class AppExceptionLogConfiguration : IEntityTypeConfiguration<AppExceptionLog>
{
    public void Configure(EntityTypeBuilder<AppExceptionLog> builder)
    {
        builder.ToTable("AppExceptionLogs");
        builder.Property(e => e.Message).HasMaxLength(1000).IsRequired();
        builder.Property(e => e.ExceptionType).HasMaxLength(300);
        builder.Property(e => e.Source).HasMaxLength(300);
        builder.Property(e => e.RequestPath).HasMaxLength(500);
        builder.Property(e => e.IpAddress).HasMaxLength(45);
        builder.Property(e => e.UserAgent).HasMaxLength(512);
    }
}
