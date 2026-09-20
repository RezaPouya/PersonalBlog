using Utilities.Dtos;

namespace AppServices.Admin.Lookups;

// این کوئری قبلاً فقط یک کلاس خالی بدون IQuery و بدون Handler بود (پیاده‌سازی نشده)؛
// اینجا کامل شد تا برای انتخاب پست از لیست هنگام افزودن به دوره استفاده شود.
public class GetPostsForCourseLookupQuery : IQuery<List<IdTitleDto<int>>>
{
}
