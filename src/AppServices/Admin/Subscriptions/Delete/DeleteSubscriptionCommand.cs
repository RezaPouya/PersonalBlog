using System.ComponentModel.DataAnnotations;

namespace AppServices.Admin.Subscriptions.Delete;

public class DeleteSubscriptionCommand : ICommand<int>
{
    [Required(ErrorMessage = "شناسه اجباری است")]
    public int Id { get; set; }
}
