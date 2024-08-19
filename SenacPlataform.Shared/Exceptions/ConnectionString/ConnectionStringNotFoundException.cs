namespace SenacPlataform.Shared.Exceptions.ConnectionString;

public class ConnectionStringNotFoundException(string? message) : ApplicationException(message)
{
}
