using PersonalBlog.Domain.Entities.ContactMessages;
using PersonalBlog.Domain.Entities.ContactMessages.Dtos;
using Utilities.Dtos;

namespace AppServices.Admin.ContactMessages.Grid;

public class GetContactMessageListAsGridQueryHandler(IContactMessageRepository repository)
    : IQueryHandler<GetContactMessageListAsGridQuery, GridDataSourceResult<ContactMessageGridDto>>
{
    public async Task<GridDataSourceResult<ContactMessageGridDto>> Handle(GetContactMessageListAsGridQuery input, CancellationToken cancellationToken)
    {
        return await repository.GetGridAsync(input, cancellationToken);
    }
}
