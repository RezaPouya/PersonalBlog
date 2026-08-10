namespace PersonalBlog.Domain.Commons;

/// <summary>
/// عیناً هم‌قرارداد با ILocalCacheManager پروژه‌ی نمونه (PersonalBlog)، اما برای رعایت
/// جهت وابستگی‌های Clean Architecture، اینترفیس در Domain قرار گرفته و پیاده‌سازی
/// (LocalCacheManager با IMemoryCache) در Infrastructure است.
/// </summary>
public interface ILocalCacheManager
{
    void ClearAll();

    Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> factory, int timeOutInSeconds,
        CancellationToken cancellationToken = default);

    void Remove(string key);
    void Set<T>(string key, T value, int timeoutInSeconds);
    bool TryGet<T>(string key, out T? value);
}
