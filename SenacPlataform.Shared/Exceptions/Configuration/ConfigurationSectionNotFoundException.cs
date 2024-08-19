namespace SenacPlataform.Shared.Exceptions.Configuration;

public class ConfigurationSectionNotFoundException : ApplicationException
{
    public ConfigurationSectionNotFoundException(string? message) : base(message)
    {
    }
}
