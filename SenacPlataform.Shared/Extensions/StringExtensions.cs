namespace SenacPlataform.Shared.Extensions;
public static class StringExtensions
{
    public static bool IsEmpty(this string value)
    {
        return string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value);
    }

    public static (string FirstName, string LastName) GetFirstAndLastName(this string fullName)
    {
        if (string.IsNullOrWhiteSpace(fullName))
        {
            return (string.Empty, string.Empty);
        }

        var nameParts = fullName.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);

        if (nameParts.Length == 1)
        {
            return (nameParts[0], string.Empty);
        }

        var firstName = nameParts[0];
        var lastName = nameParts[^1];

        return (firstName, lastName);
    }

}
