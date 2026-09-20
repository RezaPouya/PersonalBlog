using PersonalBlog.Domain.Entities.Subscriptions;
using PersonalBlog.Domain.Entities.Subscriptions.Dtos;
using Utilities.Dtos;

namespace Infrastructure.Persistence.SqlServer.Repositories;

public class SubscriptionRepository(AppDbContext dbContext) : RepositoryBase<Subscription>(dbContext), ISubscriptionRepository
{
    public async Task<GridDataSourceResult<SubscriptionGridDto>> GetGridAsync(GridDataSourceRequest request, CancellationToken cancellationToken)
    {
        var query = base.DbContext.Subscriptions.AsNoTracking()
            .Select(s => new SubscriptionGridDto
            {
                Id = s.Id,
                Email = s.Email,
                IsActive = s.IsActive,
                UnsubscribedAt = s.UnsubscribedAt,
                CreatedAt = s.CreatedAt
            });

        return await query.ToDataSourceResult(request, cancellationToken);
    }

    public async Task<bool> IsExistsByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await base.DbContext.Subscriptions.AsNoTracking()
            .AnyAsync(s => s.Email == email && s.IsActive, cancellationToken);
    }

    public async Task<int> GetActiveCountAsync(CancellationToken cancellationToken)
    {
        return await base.DbContext.Subscriptions.AsNoTracking().CountAsync(s => s.IsActive, cancellationToken);
    }
}
