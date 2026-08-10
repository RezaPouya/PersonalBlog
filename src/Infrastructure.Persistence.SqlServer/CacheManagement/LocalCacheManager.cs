using Microsoft.Extensions.Caching.Memory;
using PersonalBlog.Domain.Commons;

namespace Infrastructure.Persistence.SqlServer.CacheManagement;

/// <summary>
/// پیاده‌سازی عیناً مطابق LocalCacheManager پروژه‌ی نمونه (بر پایه‌ی IMemoryCache).
/// چون سرور منابع محدودی دارد (۱-۲ گیگ رم)، فعلاً از کش محلی درون‌فرآیندی استفاده
/// می‌شود؛ در صورت نیاز به مقیاس‌پذیری افقی، این کلاس با IDistributedCache/Redis
/// جایگزین می‌شود بدون تغییر در لایه‌های بالاتر (چون فقط ILocalCacheManager تزریق می‌شود).
/// </summary>
public class LocalCacheManager(IMemoryCache memoryCache) : ILocalCacheManager
{
    private readonly SemaphoreSlim _semaphore = new(1, 1);
    private readonly List<string> _cacheKeys = new();

    public void ClearAll()
    {
        foreach (var key in _cacheKeys)
            memoryCache.Remove(key);

        _cacheKeys.Clear();
    }

    public async Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> factory, int timeOutInSeconds,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(key))
            throw new ArgumentException("کلید کش نمی‌تواند خالی باشد.", nameof(key));

        if (memoryCache.TryGetValue(key, out T? cachedValue) && cachedValue is not null)
            return cachedValue;

        await _semaphore.WaitAsync(cancellationToken);
        try
        {
            if (memoryCache.TryGetValue(key, out cachedValue) && cachedValue is not null)
                return cachedValue;

            var value = await factory().ConfigureAwait(false);

            memoryCache.Set(key, value, new MemoryCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromSeconds(timeOutInSeconds)
            });

            _cacheKeys.Add(key);
            return value;
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public void Remove(string key)
    {
        if (key is null) return;

        memoryCache.Remove(key);
        _cacheKeys.Remove(key);
    }

    public void Set<T>(string key, T value, int timeoutInSeconds)
    {
        if (string.IsNullOrEmpty(key)) return;

        memoryCache.Set(key, value, new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(timeoutInSeconds)
        });

        _cacheKeys.Add(key);
    }

    public bool TryGet<T>(string key, out T? value)
    {
        if (string.IsNullOrEmpty(key))
        {
            value = default;
            return false;
        }

        return memoryCache.TryGetValue(key, out value);
    }
}
