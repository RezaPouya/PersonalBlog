using System.ComponentModel.DataAnnotations;
using Utilities.Extensions;

namespace AppServices.Site.ContactMessages;

public class CreateContactMessageCommand : ICommand<int>
{
    [Required(ErrorMessage = "نام اجباری است")]
    [MaxLength(150)]
    public string FullName { get; set; } = default!;

    [Required(ErrorMessage = "ایمیل اجباری است")]
    [EmailAddress(ErrorMessage = "ایمیل معتبر نیست")]
    [MaxLength(200)]
    public string Email { get; set; } = default!;

    [MaxLength(30)]
    public string? Cellphone { get; set; }

    [Required(ErrorMessage = "موضوع اجباری است")]
    [MaxLength(200)]
    public string Subject { get; set; } = default!;

    [Required(ErrorMessage = "متن پیام اجباری است")]
    [MaxLength(3000)]
    public string Body { get; set; } = default!;

    [Required(ErrorMessage = "پاسخ سؤال امنیتی را وارد کنید")]
    public string CaptchaId { get; set; } = default!;
    [Required(ErrorMessage = "پاسخ سؤال امنیتی را وارد کنید")]
    public string CaptchaAnswer { get; set; } = default!;

    public string? IpAddress { get; set; }

    public CreateContactMessageCommand Sanitize()
    {
        FullName = FullName.StringNormalization();
        Email = Email.StringNormalization();
        Subject = Subject.StringNormalization();
        Body = Body.StringNormalization();
        return this;
    }
}
