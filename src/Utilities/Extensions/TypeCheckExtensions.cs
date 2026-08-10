namespace PersonalBlog.Utilities.Extensions;

public static class TypeCheckExtensions
{
    public static bool IsDate(this object obj)
    {
        if (obj == null) return false;

        try
        {
            string strDate = obj.ToString();

            DateTime dt = DateTime.Parse(strDate);
            if (dt != DateTime.MinValue && dt != DateTime.MaxValue)
                return true;
            return false;
        }
        catch
        {
            return false;
        }
    }

    public static bool IsNum(this string str)
    {
        if (string.IsNullOrEmpty(str)) return false;

        bool isNum;
        double retNum;
        isNum = Double.TryParse(str, System.Globalization.NumberStyles.Any,
            System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
        return isNum;
    }

    public static bool IsGuid(this string str, out Guid theGuid)
    {
        if (string.IsNullOrEmpty(str))
        {
            theGuid = Guid.Empty;
            return false;
        }

        return Guid.TryParse(str, out theGuid);
    }

    public static bool IsBoolean(this string str)
    {
        if (bool.TryParse(str, out _))
        {
            return true;
        }

        return false;
    }
}