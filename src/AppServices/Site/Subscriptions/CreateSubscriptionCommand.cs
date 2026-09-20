using System.ComponentModel.DataAnnotations;
using Utilities.Extensions;

namespace AppServices.Site.Subscriptions;

public class CreateSubscriptionCommand : ICommand<int>
{
    [Required(ErrorMessage = "ایمیل اجباری است")]
    [EmailAddress(ErrorMessage = "ایمیل معتبر نیست")]
    [MaxLength(200)]
    public string Email { get; set; } = default!;

    public CreateSubscriptionCommand Sanitize()
    {
        Email = Email.StringNormalization();
        return this;
    }
}
