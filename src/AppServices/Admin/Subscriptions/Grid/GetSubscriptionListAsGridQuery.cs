using PersonalBlog.Domain.Entities.Subscriptions.Dtos;
using Utilities.Dtos;

namespace AppServices.Admin.Subscriptions.Grid;

public class GetSubscriptionListAsGridQuery : GridDataSourceRequest, IQuery<GridDataSourceResult<SubscriptionGridDto>>
{
}
