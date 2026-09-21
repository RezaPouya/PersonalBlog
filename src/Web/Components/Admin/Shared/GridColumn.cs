using Microsoft.AspNetCore.Components;

namespace Web.Components.Admin.Shared;

/// <summary>
/// تعریف یک ستون برای <see cref="DynamicGrid{TItem}"/>.
/// PropertyName باید دقیقاً همان نام پراپرتی در کلاس Dto (روی سرور) باشد،
/// چون همین مقدار برای Sort/Filter به بک‌اند (GridDataSourceRequest) ارسال می‌شود.
/// </summary>
public class GridColumn<TItem>
{
    public string Header { get; set; } = default!;

    /// <summary>نام دقیق پراپرتی روی Dto (برای فیلتر/سورت سمت سرور). اگر ستون صرفاً نمایشی است، خالی بگذارید.</summary>
    public string? PropertyName { get; set; }

    public bool Sortable { get; set; }
    public bool Filterable { get; set; }

    /// <summary>یکی از مقادیر GridFilterOperationConstants (پیش‌فرض: contains).</summary>
    public string FilterOperation { get; set; } = "contains";

    public string? Width { get; set; }

    /// <summary>برای نمایش سفارشی سلول (بج وضعیت، لینک و ...).</summary>
    public RenderFragment<TItem>? CellTemplate { get; set; }

    /// <summary>برای نمایش متنی ساده (وقتی نیاز به قالب خاصی نیست).</summary>
    public Func<TItem, string?>? CellText { get; set; }
}
