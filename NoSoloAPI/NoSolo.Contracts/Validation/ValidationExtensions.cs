using System.Text.RegularExpressions;

namespace NoSolo.Contracts.Validation;

public static class ValidationExtensions
{
    public static bool IsValidPassword(this string password)
    {
        return Regex.IsMatch(password, "([0-9]+[a-zA-Z]+[0-9a-zA-Z]*)|([a-zA-Z]+[0-9]+[0-9a-zA-Z]*)");
    }
}