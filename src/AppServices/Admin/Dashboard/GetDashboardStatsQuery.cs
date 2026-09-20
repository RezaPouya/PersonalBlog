namespace AppServices.Admin.Dashboard;

public class GetDashboardStatsQuery : IQuery<DashboardStatsDto>
{
}

public class DashboardStatsDto
{
    public int PostsCount { get; set; }
    public int CategoriesCount { get; set; }
    public int TagsCount { get; set; }
    public int CoursesCount { get; set; }
    public int ProjectsCount { get; set; }
    public int PendingCommentsCount { get; set; }
    public int UnreadMessagesCount { get; set; }
    public int ActiveSubscribersCount { get; set; }
}
