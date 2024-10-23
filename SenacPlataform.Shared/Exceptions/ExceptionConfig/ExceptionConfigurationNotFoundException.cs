namespace SenacPlataform.Shared.Exceptions.ExceptionConfig;

public class ExceptionConfigurationNotFoundException : ApplicationException
{
    public ExceptionConfigurationNotFoundException(string? message) : base(message)
    {
    }
}
