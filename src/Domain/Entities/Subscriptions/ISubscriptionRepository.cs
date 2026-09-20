using PersonalBlog.Domain.Commons;
using PersonalBlog.Domain.Entities.Subscriptions.Dtos;
using Utilities.Dtos;

namespace PersonalBlog.Domain.Entities.Subscriptions;

public interface ISubscriptionRepository : IRepository<Subscription>
{
    Task<GridDataSourceResult<SubscriptionGridDto>> GetGridAsync(GridDataSourceRequest request, CancellationToken cancellationToken);
    Task<bool> IsExistsByEmailAsync(string email, CancellationToken cancellationToken);
    Task<int> GetActiveCountAsync(CancellationToken cancellationToken);
}
