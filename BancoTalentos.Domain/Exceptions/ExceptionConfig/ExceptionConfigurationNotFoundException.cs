namespace BancoTalentos.Domain.Exceptions.ExceptionConfig;

public class ExceptionConfigurationNotFoundException : ApplicationException
{
    public ExceptionConfigurationNotFoundException(string? message) : base(message)
    {
    }
}
