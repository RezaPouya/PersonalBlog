using PersonalBlog.Domain.Entities.ContactMessages.Dtos;
using Utilities.Dtos;

namespace AppServices.Admin.ContactMessages.Grid;

public class GetContactMessageListAsGridQuery : GridDataSourceRequest, IQuery<GridDataSourceResult<ContactMessageGridDto>>
{
}
