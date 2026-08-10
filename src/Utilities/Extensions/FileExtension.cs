using PersonalBlog.Utilities.Extensions;

namespace Iau.IT.Utility.Extensions;

public static partial class FileExtension
{
    public static string GetExtension(this string base64)
    {
        return (base64?[..5]?.ToUpper()) switch
        {
            "IVBOR" => ".png",
            "/9J/4" => ".jpg",
            "AAAAF" => ".mp4",
            "JVBER" => ".pdf",
            "AAABA" => ".ico",
            "UMFYI" => ".rar",
            "E1XYD" => ".rtf",
            "U1PKC" => ".txt",
            "MQOWM" or "77U/M" => ".srt",
            "2QNYR" => ".csv",
            "UESDB" => ".xlsx",
            _ => null,
        };
    }

    public static string GetMimeType(this string extension)
    {
        return extension.GetMimeTypeFromMappings();
    }

    public static string NormalizeBase64(this string base64)
    {
        var hasTypeFlag = base64.Substring(0, Math.Min(base64.Length, 20)).Contains(';');
        if (hasTypeFlag)
        {
            var index = base64.IndexOf(";base64,");
            return base64.Substring(index + 8, base64.Length - index - 8);
        }
        return base64;
    }

    public static byte[] GetBytes(this string base64)
    {
        if (base64 == null)
        {
            return null;
        }
        return Convert.FromBase64String(base64);
    }

    public static string GetFileSize(this long len)
    {
        string[] sizes = { "B", "KB", "MB", "GB", "TB" };
        int order = 0;

        while (len >= 1024 && order < sizes.Length - 1)
        {
            order++;
            len = len / 1024;
        }

        // Adjust the format string to your preferences. For example "{0:0.#}{1}" would
        // show a single decimal place, and no space.
        string result = String.Format("{0:0.##} {1}", len, sizes[order]);

        return result;
    }

    public static string GetFileSizePersian(this long len)
    {
        string[] sizes = { "بایت", "کیلوبایت", "مگابایت", "گیگابایت", "ترابایت" };
        int order = 0;

        while (len >= 1024 && order < sizes.Length - 1)
        {
            order++;
            len = len / 1024;
        }

        string result = String.Format("{0:0.##} {1}", len, sizes[order]);

        return result;
    }
}