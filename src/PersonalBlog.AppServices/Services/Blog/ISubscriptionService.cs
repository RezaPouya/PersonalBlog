using PersonalBlog.AppServices.Dtos;

namespace PersonalBlog.AppServices.Services.Blog;

public interface ISubscriptionService
{
    Task SubscribeAsync(CreateSubscriptionInputDto input, CancellationToken cancellationToken = default);
    Task UnsubscribeAsync(string email, CancellationToken cancellationToken = default);
    Task<List<SubscriptionDto>> GetActiveAsync(CancellationToken cancellationToken = default);
    Task SendNewsletterAsync(string subject, string htmlBody, CancellationToken cancellationToken = default);
}
