using System.ComponentModel.DataAnnotations;
using Utilities.Extensions;

namespace AppServices.Site.Comments;

public class CreateCommentCommand : ICommand<int>
{
    [Required] public int PostId { get; set; }
    public int? ParentCommentId { get; set; }

    [Required(ErrorMessage = "نام اجباری است")]
    [MaxLength(100)]
    public string DisplayName { get; set; } = default!;

    [Required(ErrorMessage = "ایمیل اجباری است")]
    [EmailAddress(ErrorMessage = "ایمیل معتبر نیست")]
    [MaxLength(200)]
    public string Email { get; set; } = default!;

    [Required(ErrorMessage = "متن کامنت اجباری است")]
    [MaxLength(2000)]
    public string Content { get; set; } = default!;

    // کپچا (سمت سرور اعتبارسنجی می‌شود، ذخیره نمی‌شود)
    [Required(ErrorMessage = "پاسخ سؤال امنیتی را وارد کنید")]
    public string CaptchaId { get; set; } = default!;
    [Required(ErrorMessage = "پاسخ سؤال امنیتی را وارد کنید")]
    public string CaptchaAnswer { get; set; } = default!;

    public string? IpAddress { get; set; }

    public CreateCommentCommand Sanitize()
    {
        DisplayName = DisplayName.StringNormalization();
        Email = Email.StringNormalization();
        Content = Content.StringNormalization();
        return this;
    }
}
