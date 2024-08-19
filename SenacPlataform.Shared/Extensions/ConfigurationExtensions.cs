using Microsoft.Extensions.Configuration;
using SenacPlataform.Shared.Exceptions.Configuration;
using SenacPlataform.Shared.Exceptions.ConnectionString;

namespace SenacPlataform.Shared.Extensions;

public static class ConfigurationExtensions
{
    private const string MSG_CONFIGURACAO_NAO_ENCONTRADA = "Não foi possível encontrar a seção '{0}' da configuração buscada.";
    private const string MSG_CONNECTION_STRING_NAO_ENCONTRADA = "Não foi encontrada a string de conexão.";

    public static string SPGetConnectionString(this IConfiguration configuration)
    {
        var result = configuration.GetConnectionString("default");

        if (result is not null) return result;
        throw new ConnectionStringNotFoundException(MSG_CONNECTION_STRING_NAO_ENCONTRADA);
    }

    public static IConfigurationSection SPGetSection(this IConfiguration configuration, string key)
    {
        return configuration.GetSection(key) ?? throw new ConfigurationSectionNotFoundException(string.Format(MSG_CONFIGURACAO_NAO_ENCONTRADA, key));
    }

    public static T SPGetSection<T>(this IConfiguration configuration, string key)
    {
        return configuration.GetSection(key).Get<T>() ?? throw new ConfigurationSectionNotFoundException(string.Format(MSG_CONFIGURACAO_NAO_ENCONTRADA, key));
    }
}
