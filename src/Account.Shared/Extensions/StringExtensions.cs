using System.Globalization;
using System.Text.RegularExpressions;
using Account.Shared.Constants;

namespace Account.Shared.Extensions;
public static class StringExtensions
{
    public static bool IsNullOrEmpty(this string value)
    {
        return string.IsNullOrEmpty(value);
    }

    public static bool IsValidEmail(this string email)
    {
        return !string.IsNullOrWhiteSpace(email) &&
               Regex.IsMatch(email, ValidationConstants.Email.Pattern);
    }

    public static string ToTitleCase(this string input)
    {
        return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(input.ToLower());
    }
}