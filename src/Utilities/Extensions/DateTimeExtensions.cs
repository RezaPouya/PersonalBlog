using DNTPersianUtils.Core;
using PersonalBlog.Utilities.Dtos;
using System.Globalization;

namespace PersonalBlog.Utilities.Extensions;

public static class DateTimeExtensions
{
    public static string ConvertToPersianDate(this DateTime dateTime)
    {
        var pc = new PersianCalendar();
        return
            $"{pc.GetYear(dateTime)}/{pc.GetMonth(dateTime)}/{pc.GetDayOfMonth(dateTime)} {dateTime.Hour}:{dateTime.Minute}:{dateTime.Second}";
    }

    /// <summary>
    /// return persian date , turn it to int
    /// </summary>
    /// <param name="date"></param>
    /// <returns>14050503</returns>
    public static int ToPersianDateAsInt(this DateTime date)
    {
        return Convert.ToInt32(ToPersianDateAsStringWithougSlash(date));
    }

    public static string ToPersianDateAsStringWithougSlash(this DateTime date)
    {
        var pc = new PersianCalendar();
        var year = pc.GetYear(date).ToString();
        var month = pc.GetMonth(date).ToString().PadLeft(2, '0');
        var dayOfMonth = pc.GetDayOfMonth(date).ToString().PadLeft(2, '0');
        return $"{year}{month}{dayOfMonth}";
    }

    public static string ToTimeAsStringWithougSlash(this DateTime time)
    {
        var hour = time.Hour.ToString().PadLeft(2, '0');
        var minutes = time.Minute.ToString().PadLeft(2, '0');
        var seconds = time.Second.ToString().PadLeft(2, '0');
        return $"{hour}{minutes}{seconds}";
    }

    public static string GetShortDayOfWeekTime(this DateTime time)
    {
        // Day of week
        var dayOfWeek = new[] { "یکشنبه", "دوشنبه", "سه شنبه", "چهارشنبه", "پنج شنبه", "جمعه", "شنبه" };

        // =============================================================================
        var pc = new PersianCalendar();

        var date = dayOfWeek[(int)pc.GetDayOfWeek(DateTime.Now)] + " " +
                   pc.GetDayOfMonth(DateTime.Now);
        return date;
    }

    public static PersianDateOffsetsDto GetPersianDateOffsets(this DateTime date)
    {
        var pc = new PersianCalendar();

        var result = new PersianDateOffsetsDto();

        var dayOfPersianMonth = pc.GetDayOfMonth(date);
        result.FirstDayOfMonth = date.AddDays(1 - dayOfPersianMonth).Date;
        result.FirstDayOfNextMonth = pc.AddMonths(result.FirstDayOfMonth, 1);

        var dayOfPersianYear = pc.GetDayOfYear(date);
        result.FirstDayOfYear = date.AddDays(1 - dayOfPersianYear).Date;
        result.FirstDayOfNextYear = pc.AddYears(result.FirstDayOfYear, 1);

        return result;
    }

    public static string FormatDateToShortPersianDateTimeIncludeTimeDetails(this DateTime now)
    {
        return now.FormatDateToShortPersianDateTime() + $":{now.Second}:{now.Millisecond}";
    }
}