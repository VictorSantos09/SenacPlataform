namespace SenacPlataform.Shared.Extensions;
public static class IdExtensions
{
    public static bool IsValidIdentifier(this int id)
    {
        return id > 0;
    }

    public static bool IsNotValidIdentifier(this int id)
    {
        return !IsValidIdentifier(id);
    }
}
