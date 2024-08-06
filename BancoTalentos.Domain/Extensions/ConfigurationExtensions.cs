using BancoTalentos.Domain.Config;
using BancoTalentos.Domain.Exceptions.ExceptionConfig;
using Microsoft.Extensions.Configuration;

namespace SenacPlataform.Shared.Extensions;

public static class ConfigurationExtensions
{
    private const string CNT_EXCEPTION_CONFIG_JSON_SECTION = "exceptionConfig";
    public static ExceptionConfig GetExceptionConfig(this IConfiguration configuration)
    {
        return configuration.GetSection(CNT_EXCEPTION_CONFIG_JSON_SECTION).Get<ExceptionConfig>()
            ?? throw new ExceptionConfigurationNotFoundException("Configuração de tratamento de exceção não foi encontrada");
    }
}
