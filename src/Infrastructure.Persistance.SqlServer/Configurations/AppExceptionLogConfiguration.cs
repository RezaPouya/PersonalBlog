using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalBlog.Domain.Exceptions;

namespace Infrastructure.Persistance.SqlServer.Configurations;

public class AppExceptionLogConfiguration : IEntityTypeConfiguration<AppExceptionLog>
{
    public void Configure(EntityTypeBuilder<AppExceptionLog> builder)
    {
        builder.ToTable("AppExceptionLogs");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(p => p.CreatedAt).IsRequired();
        builder.Property(e => e.Message).IsRequired();
        builder.Property(e => e.ExceptionType).HasMaxLength(300);
        builder.Property(e => e.Source).HasMaxLength(300);
        builder.Property(e => e.RequestPath).HasMaxLength(500);
        builder.Property(e => e.IpAddress).HasMaxLength(45);
        builder.Property(e => e.UserAgent).HasMaxLength(512);
    }
}
