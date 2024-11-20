using System.Text.RegularExpressions;

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

    public static bool IsEmail(this string input)
    {
        string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(input, emailPattern);
    }

    public static bool IsPhoneNumber(this string input)
    {
        // Aceita formatos de telefone: (XX) XXXXX-XXXX, (XX) XXXX-XXXX, XXXXXXXXXX, etc.
        string phonePattern = @"^\(?\d{2}\)?\s?\d{4,5}-?\d{4}$";
        return Regex.IsMatch(input, phonePattern);
    }
}
