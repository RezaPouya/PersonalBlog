using PersonalBlog.Domain.Commons;

namespace PersonalBlog.Infrastructure.Persistences;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        await context.SaveChangesAsync(cancellationToken);
}
