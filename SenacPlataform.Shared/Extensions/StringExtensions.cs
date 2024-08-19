namespace SenacPlataform.Shared.Extensions;
public static class StringExtensions
{
    public static bool IsEmpty(this string value)
    {
        return string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value);
    }

    public static string Format(this string value, params object[] args)
    {
        return string.Format(value, args);
    }

    public static string Format(this string value, object? arg)
    {
        return string.Format(value, arg);
    }
}
