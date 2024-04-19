using System.Text.RegularExpressions;

namespace NoSolo.Contracts.Validation;

public static partial class ValidationExtensions
{
    public static bool IsValidPassword(this string password)
    {
        return MyRegex().IsMatch(password);
    }

    [GeneratedRegex("([0-9]+[a-zA-Z]+[0-9a-zA-Z]*)|([a-zA-Z]+[0-9]+[0-9a-zA-Z]*)")]
    private static partial Regex MyRegex();
}