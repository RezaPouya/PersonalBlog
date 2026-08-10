namespace PersonalBlog.Utilities.Extensions;

public static class ErrorMessageExtension
{
    public static string GetNullMessage(this string fieldName)
    {
        var nullMessageFormat = "مقدار {0} را وارد کنید";
        return string.Format(nullMessageFormat, fieldName);
    }

    public static string GetInvalidMessage(this string fieldName)
    {
        var invalidMessageFormat = "مقدار {0} نا معتبر می باشد";
        return string.Format(invalidMessageFormat, fieldName);
    }
}