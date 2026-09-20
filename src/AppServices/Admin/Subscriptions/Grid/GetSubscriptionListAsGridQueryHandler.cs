using PersonalBlog.Domain.Entities.Subscriptions;
using PersonalBlog.Domain.Entities.Subscriptions.Dtos;
using Utilities.Dtos;

namespace AppServices.Admin.Subscriptions.Grid;

public class GetSubscriptionListAsGridQueryHandler(ISubscriptionRepository repository)
    : IQueryHandler<GetSubscriptionListAsGridQuery, GridDataSourceResult<SubscriptionGridDto>>
{
    public async Task<GridDataSourceResult<SubscriptionGridDto>> Handle(GetSubscriptionListAsGridQuery input, CancellationToken cancellationToken)
    {
        return await repository.GetGridAsync(input, cancellationToken);
    }
}
