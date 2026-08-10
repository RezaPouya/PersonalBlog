using DNTPersianUtils.Core;
using DNTPersianUtils.Core.Normalizer;
using System.Text.RegularExpressions;

namespace PersonalBlog.Utilities.Extensions;

public static partial class StringExtensions
{
    public static bool IsValidEmail(this string email)
    {
        var trimmedEmail = email.Trim();

        if (trimmedEmail.EndsWith("."))
            return false; // suggested by @TK-421

        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == trimmedEmail;
        }
        catch
        {
            return false;
        }
    }

    public static List<string> ConvertCommaSeparatedStringToListOfString(this string column)
    {
        return string.IsNullOrEmpty(column) ? new List<string>() : column.Split(',').ToList();
    }

    public static string ConvertListOfStringToCommaSeparatedString(this List<string> prop)
    {
        if (prop is null)
            return string.Empty;

        return prop.Any() ? string.Join(',', prop) : "";
    }

    public static string NormalizeMobile(this string mobile)
    {
        mobile = mobile?.Trim().ToEnglishNumbers();

        if (mobile.StartsWith("09"))
            return mobile;

        var firstDigitIndex = mobile.IndexOf("9");

        if (firstDigitIndex == -1)
        {
            return mobile;
        }

        return string.Concat("0", mobile.AsSpan(firstDigitIndex, mobile.Length - firstDigitIndex));
    }

    public static bool IsMobile(this string normalizedMobile)
    {
        var hasLength11 = normalizedMobile.Length == 11;
        var isOnlyDigitChars = !normalizedMobile.Any(m => !char.IsDigit(m));
        var startsWith09 = normalizedMobile.StartsWith("09");

        return hasLength11 && isOnlyDigitChars && startsWith09;
    }

    public static bool IsSafePassword(this string password)
    {
        password = password?.Trim();

        return
            password != null &&
            password.Length > 6 &&
            !password.Any(x => x == ' ') &&
            password.Any(x => char.IsDigit(x)) &&
            password.Any(x => char.IsUpper(x)) &&
            password.Any(x => char.IsLower(x)) &&
            password.Length < 16 &&
            !password.Any(x => !char.IsAscii(x));
    }

    public static bool IsPhone(this string normalizedPhone)
    {
        var isOnlyDigitChars = !normalizedPhone.Any(m => !char.IsDigit(m));

        return isOnlyDigitChars;
    }

    public static bool IsFax(this string fax)
    {
        var hasValidChars = !fax.Any(m => !char.IsDigit(m) && m != '(' && m != ')' && m != '+');
        return hasValidChars;
    }

    public static bool HasUnicodeChar(this string text) => text.Any(c => c > 255);

    public static bool IsEmail(this string normalizedEmail)
    {
        var match = Regex.IsMatch(normalizedEmail,
            "^([\\w\\.\\-]+)@([\\w\\-]+)((\\.(\\w){2,3})+)$");
        return match;
    }

    public static string GenerateRandomString(int length)
    {
        var random = new Random();
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    public static string StringNormalization(this string str, bool toLower = false)
    {
        string res = str?.Trim()?.ApplyCorrectYeKe()?.RemoveDiacritics()?.ToEnglishNumbers();

        if (toLower)
            res = res?.ToLower();

        return res;
    }

    // public static string RandomOtp(int length)
    // {
    //     var rand = new Random();
    //     var number = 0;
    //     for (var i = 0; i < length; i++)
    //     {
    //         var digit = rand.Next(0, 9);
    //         number += (int) Math.Pow(10, i) * digit;
    //     }
    //
    //     return number.ToString();
    // }

    /// <summary>
    /// Trim - ApplyCorrectYeKe - RemoveDiacritics - ToEnglishNumbers
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string TitleNormalization(this string str, bool toLower = false)
    {
        string res = str?.Trim()?.ApplyCorrectYeKe()?.RemoveDiacritics()?.ToEnglishNumbers();

        if (toLower)
            res = res?.ToLower();

        return res;
    }

    // Helper method to escape full-text search terms
    public static string EscapeFullTextSearchTerm(this string searchTerm)
    {
        if (string.IsNullOrEmpty(searchTerm))
            return searchTerm;

        // List of full-text search special characters that need escaping
        var specialCharacters = new[] { '"', '\'', '(', ')', ',', '.', '!', '?', ';', ':', '-', '&', '|', '~', '^', '$', '*', '+', '=', '<', '>', '[', ']', '{', '}' };

        // Escape special characters by wrapping in quotes
        if (searchTerm.Contains(' ') || searchTerm.Any(c => specialCharacters.Contains(c)))
        {
            return $"{searchTerm.Replace("\"", "\"\"")}";
        }

        return searchTerm;
    }

    public static string ReplaceMultipleSpaceWithSingleOne(this string str)
    {
        str = str?.Trim();
        RegexOptions options = RegexOptions.None;
        Regex regex = new Regex("[ ]{2,}", options);
        str = regex.Replace(str, " ");
        return str;
    }
}