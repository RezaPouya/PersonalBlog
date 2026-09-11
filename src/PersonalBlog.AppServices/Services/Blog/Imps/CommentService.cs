using Microsoft.EntityFrameworkCore;
using PersonalBlog.AppServices.Dtos;
using PersonalBlog.AppServices.Services.Common;
using PersonalBlog.Domain.Commons;
using PersonalBlog.Domain.Constants;
using PersonalBlog.Domain.Entities.Blog;

namespace PersonalBlog.AppServices.Services.Blog.Imps;

public class CommentService(
    IRepository<Comment> repository,
    IRepository<Post> postRepository,
    IUnitOfWork unitOfWork,
    ICaptchaService captchaService,
    ILocalCacheManager cache) : ICommentService
{
    public async Task<long> SubmitAsync(CreateCommentInputDto input, string ipAddress,
        CancellationToken cancellationToken = default)
    {
        var captchaOk = await captchaService.ValidateAsync(input.RecaptchaToken, cancellationToken);

        var entity = new Comment
        {
            PostId = input.PostId,
            ParentCommentId = input.ParentCommentId,
            DisplayName = input.DisplayName,
            Email = input.Email,
            Content = input.Content,
            IpAddress = ipAddress,
            IsSpam = !captchaOk,
            IsApproved = false // همیشه با تأیید ادمین منتشر می‌شود
        };

        repository.Add(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var post = await postRepository.FindByIdAsync(input.PostId);
        if (post != null)
            cache.Remove(CacheKeys.PostBySlug(post.Slug));

        return entity.Id;
    }

    public async Task<List<CommentDto>> GetPendingAsync(CancellationToken cancellationToken = default)
    {
        return await repository.Query()
            .Where(c => !c.IsApproved && !c.IsSpam)
            .OrderBy(c => c.CreatedAt)
            .Select(c => new CommentDto
            {
                Id = c.Id,
                DisplayName = c.DisplayName,
                Content = c.Content,
                CreatedAt = c.CreatedAt,
                IsApproved = c.IsApproved
            })
            .ToListAsync(cancellationToken);
    }

    public async Task ApproveAsync(long id, CancellationToken cancellationToken = default)
    {
        var entity = await repository.FindByIdAsync(id);
        if (entity is null) return;

        entity.IsApproved = true;
        repository.Update(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        await InvalidatePostCommentsCacheAsync(entity.PostId, cancellationToken);
    }

    public async Task RejectAsync(long id, CancellationToken cancellationToken = default)
    {
        var entity = await repository.FindByIdAsync(id);
        if (entity is null) return;

        entity.IsSpam = true;
        entity.IsApproved = false;
        repository.Update(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(long id, CancellationToken cancellationToken = default)
    {
        var entity = await repository.FindByIdAsync(id);
        if (entity is null) return;

        repository.Delete(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        await InvalidatePostCommentsCacheAsync(entity.PostId, cancellationToken);
    }

    private async Task InvalidatePostCommentsCacheAsync(long postId, CancellationToken cancellationToken)
    {
        var post = await postRepository.FindByIdAsync(postId);
        if (post != null)
            cache.Remove(CacheKeys.PostBySlug(post.Slug));
    }
}
