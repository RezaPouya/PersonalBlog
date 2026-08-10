using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using PersonalBlog.Domain.Entities;
using PersonalBlog.Domain.Exceptions;

namespace Infrastructure.Persistance.SqlServer;

public class AppDbContext(DbContextOptions<AppDbContext> options)
    : IdentityDbContext<AppUser, AppRole, int>(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }

    #region Blog

    public DbSet<Category> Categories { get; set; } = default!;
    public DbSet<Tag> Tags { get; set; } = default!;
    public DbSet<Post> Posts { get; set; } = default!;
    public DbSet<PostTag> PostTags { get; set; } = default!;
    public DbSet<PostVisit> PostVisits { get; set; } = default!;
    public DbSet<Comment> Comments { get; set; } = default!;
    public DbSet<Course> Courses { get; set; } = default!;
    public DbSet<CoursePost> CoursesPosts { get; set; } = default!;
    public DbSet<Project> Projects { get; set; } = default!;
    public DbSet<ContactMessage> ContactMessages { get; set; } = default!;
    public DbSet<Subscription> Subscriptions { get; set; } = default!;

    #endregion

    public DbSet<AppExceptionLog> AppExceptionLogs { get; set; } = default!;
}
