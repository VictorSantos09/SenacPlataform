using Microsoft.Extensions.Configuration;
using SenacPlataform.Shared.Config;
using SenacPlataform.Shared.Exceptions.ExceptionConfig;

namespace SenacPlataform.Shared.Extensions;

public static class ConfigurationExtensions
{
    private const string CNT_EXCEPTION_CONFIG_JSON_SECTION = "exceptionConfig";
    private const string CNT_NOME_CONNECTION_STRING = "Default";
    
    public static ExceptionConfig SNGetExceptionConfig(this IConfiguration configuration)
    {
        return configuration.GetSection(CNT_EXCEPTION_CONFIG_JSON_SECTION).Get<ExceptionConfig>()
            ?? throw new ExceptionConfigurationNotFoundException("Configuração de tratamento de exceção não foi encontrada");
    }

    public static string? SNGetConnectionString(this IConfiguration configuration)
    {
        return configuration.GetConnectionString(CNT_NOME_CONNECTION_STRING) 
            ?? throw new InvalidOperationException($"string de conexão (ConnecionString) com o nome '{CNT_NOME_CONNECTION_STRING}' não foi encontrada.");
    }
}
