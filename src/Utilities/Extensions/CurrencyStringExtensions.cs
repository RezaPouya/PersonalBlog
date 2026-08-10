namespace PersonalBlog.Utilities.Extensions;

public static class CurrencyStringExtensions
{
    public static string FormatRialToTomanString(this long? price, bool showToman = true)
    {
        if (price is null)
            return string.Empty;

        if (price.Value < 0)
        {
            price = Math.Abs(price.Value) / 10;

            if (price < 10)
                price = 10;

            price = (long)(price / 10);
        }

        var priceStr = price.Value.ToString("N0");

        var result = showToman ? $"{priceStr} تومان" : priceStr;

        return result.Trim();
    }

    public static string GetCurrency(this long? price, bool showToman = true)
    {
        if (price is null)
            return string.Empty;

        return "تومان";
    }

    public static string FormatRialToTomanString(this long price, bool showToman = true)
    {
        long? priceS = price;
        return priceS.FormatRialToTomanString(showToman);
    }

    public static string FormatToRialString(this long? price, bool showRial = true)
    {
        if (price is null)
            return string.Empty;

        if (price.Value < 0)
        {
            price = Math.Abs(price.Value) / 10;
        }

        var priceStr = price.Value.ToString("N0");

        var result = showRial ? $"{priceStr} ریال" : priceStr;

        return result.Trim();
    }

    public static string FormatToRialString(this long price, bool showRial = true)
    {
        long? priceS = price;
        return priceS.FormatRialToTomanString(showRial);
    }
}