namespace SenacPlataform.Shared.Messages;
public static class MessageBase
{
    public static string GetErrorMessage<TError>(Dictionary<TError, string> errorMessages, TError errorCode) where TError : Enum
    {
        var encontrado = errorMessages.TryGetValue(errorCode, out var message);

        return encontrado ? $"{errorCode} - {message}" : $"Erro com código {errorCode} não encontrado.";
    }

    public static bool IsDefined<TEnum>(TEnum value) where TEnum : Enum
    {
        return Enum.IsDefined(typeof(TEnum), value);
    }
}
