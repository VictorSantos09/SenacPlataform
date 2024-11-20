using Microsoft.Extensions.Configuration;

namespace SenacPlataform.Shared.Extensions;

public static class ConfigurationBuilderExtensions
{
    // Consultar -> https://andrewlock.net/sharing-appsettings-json-configuration-files-between-projects-in-asp-net-core/#the-initial-setup

    private static readonly string APPSETTINGS_PATH = $"{TryGetSolutionDirectory()}/appsettings.json";
    private static readonly string APPSETTINGS_DEVELOPMENT_PATH = $"{TryGetSolutionDirectory()}/appsettings.development.json";
    private const string NOME_PROJETO_SHARED = "SenacPlataform.Shared";

    public static IConfigurationBuilder SNConfigureAppSettings(this IConfigurationBuilder builder)
    {
        builder.AddJsonFile(APPSETTINGS_PATH, optional: false, reloadOnChange: false);
        builder.AddJsonFile(APPSETTINGS_DEVELOPMENT_PATH, optional: true, reloadOnChange: false);
        return builder;
    }

    private static DirectoryInfo TryGetSolutionDirectory()
    {
        return new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), "..", NOME_PROJETO_SHARED));
    }
}
