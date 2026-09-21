using AppServices.Commons;
using FluentValidation;
using PersonalBlog.Domain.Commons;
using PersonalBlog.Domain.Entities.Comments;
using PersonalBlog.Domain.Entities.Posts;
using PersonalBlog.Domain.Exceptions;

namespace AppServices.Site.Comments;

public class CreateCommentCommandHandler(
    IValidator<CreateCommentCommand> validator,
    ICaptchaService captchaService,
    IPostRepository postRepository,
    ICommentRepository commentRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<CreateCommentCommand, int>
{
    public async Task<int> Handle(CreateCommentCommand input, CancellationToken cancellationToken)
    {
        var validationResult = validator.Validate(input);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        input.Sanitize();

        var isCaptchaValid = await captchaService.ValidateAsync(input.CaptchaId, input.CaptchaAnswer, cancellationToken);
        if (!isCaptchaValid)
            throw new BusinessException("پاسخ سؤال امنیتی درست نیست.");

        var post = await postRepository.GetByIdAsync(input.PostId, cancellationToken)
            ?? throw new BusinessException("پست یافت نشد.");

        if (!post.IsPublished || !post.IsCommentsEnabled)
            throw new BusinessException("ارسال کامنت برای این پست امکان‌پذیر نیست.");

        var comment = new Comment
        {
            PostId = input.PostId,
            ParentCommentId = input.ParentCommentId,
            DisplayName = input.DisplayName,
            Email = input.Email,
            Content = input.Content,
            IpAddress = input.IpAddress,
            // برای جلوگیری از اسپم، کامنت‌ها به‌صورت پیش‌فرض تا تأیید در پنل ادمین منتشر نمی‌شوند.
            IsApproved = false
        };

        commentRepository.Create(comment);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return comment.Id;
    }
}
