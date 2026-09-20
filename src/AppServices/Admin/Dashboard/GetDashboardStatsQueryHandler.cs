using PersonalBlog.Domain.Entities.Categories;
using PersonalBlog.Domain.Entities.Comments;
using PersonalBlog.Domain.Entities.ContactMessages;
using PersonalBlog.Domain.Entities.Courses;
using PersonalBlog.Domain.Entities.Posts;
using PersonalBlog.Domain.Entities.Projects;
using PersonalBlog.Domain.Entities.Subscriptions;
using PersonalBlog.Domain.Entities.Tags;
using Utilities.Dtos;

namespace AppServices.Admin.Dashboard;

public class GetDashboardStatsQueryHandler(
    IPostRepository postRepository,
    ICategoryRepository categoryRepository,
    ITagRepository tagRepository,
    ICourseRepository courseRepository,
    IProjectRepository projectRepository,
    ICommentRepository commentRepository,
    IContactMessageRepository contactMessageRepository,
    ISubscriptionRepository subscriptionRepository) : IQueryHandler<GetDashboardStatsQuery, DashboardStatsDto>
{
    public async Task<DashboardStatsDto> Handle(GetDashboardStatsQuery input, CancellationToken cancellationToken)
    {
        // برای شمارش صرف، از همان گرید‌های موجود با PageSize=1 استفاده می‌کنیم
        // (نیازی به افزودن متد Count جداگانه به هر ریپازیتوری نیست).
        var tinyRequest = new GridDataSourceRequest(1, 1);

        var postsTotal = (await postRepository.GetGridAsync(tinyRequest, cancellationToken)).Total;
        var categoriesTotal = (await categoryRepository.GetCategoryGridAsync(tinyRequest, cancellationToken)).Total;
        var tagsTotal = (await tagRepository.GetGridAsync(tinyRequest, cancellationToken)).Total;
        var coursesTotal = (await courseRepository.GetGridAsync(tinyRequest, cancellationToken)).Total;
        var projectsTotal = (await projectRepository.GetGridAsync(tinyRequest, cancellationToken)).Total;

        var unreadMessages = await contactMessageRepository.GetUnreadCountAsync(cancellationToken);
        var activeSubscribers = await subscriptionRepository.GetActiveCountAsync(cancellationToken);

        // کامنت‌های در انتظار تأیید: با فیلتر روی گرید کامنت‌ها
        var pendingCommentsRequest = new GridDataSourceRequest(1, 1);
        pendingCommentsRequest.InputParams.Filters = new List<GridPropertyFilterDto>
        {
            new GridPropertyFilterDto { PropertyName = nameof(PersonalBlog.Domain.Entities.Comments.Dtos.CommentGridDto.IsApproved), Operation = "eq", Value = "false" }
        };
        var pendingComments = (await commentRepository.GetGridAsync(pendingCommentsRequest, cancellationToken)).Total;

        return new DashboardStatsDto
        {
            PostsCount = postsTotal,
            CategoriesCount = categoriesTotal,
            TagsCount = tagsTotal,
            CoursesCount = coursesTotal,
            ProjectsCount = projectsTotal,
            PendingCommentsCount = pendingComments,
            UnreadMessagesCount = unreadMessages,
            ActiveSubscribersCount = activeSubscribers
        };
    }
}
