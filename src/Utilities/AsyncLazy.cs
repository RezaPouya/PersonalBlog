// PersonalBlog.Utilities/AsyncLazy.cs
namespace PersonalBlog.Utilities;

/// <summary>
/// یک مقدار تنبل که به صورت ناهمگام مقداردهی می‌شود.
/// </summary>
public sealed class AsyncLazy<T> : Lazy<Task<T>>
{
    public AsyncLazy(Func<Task<T>> taskFactory) : base(() => Task.Run(taskFactory))
    {
    }

    public Task<T> GetValueAsync() => Value;
}