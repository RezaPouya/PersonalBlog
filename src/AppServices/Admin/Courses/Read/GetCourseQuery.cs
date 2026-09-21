namespace AppServices.Admin.Courses.Read;

public class GetCourseQuery : IQuery<CourseHeaderDto?>
{
    public int Id { get; set; }
}

/// <summary>فقط عنوان دوره؛ برای هدر صفحه‌ی «مطالب دوره».</summary>
public class CourseHeaderDto
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
}
