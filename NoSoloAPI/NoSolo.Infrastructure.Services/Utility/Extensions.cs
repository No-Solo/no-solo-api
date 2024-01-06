namespace NoSolo.Infrastructure.Services.Utility;

public static class Extensions
{
    public static bool IsEmpty(this object text)
    {
        return (text == null || string.IsNullOrEmpty(text.TrimIfNull()));
    }
    
    public static string TrimIfNull(this object text)
    {
        return text == null ? string.Empty : text.ToString().Trim();
    }
}