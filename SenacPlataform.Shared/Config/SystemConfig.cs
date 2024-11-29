using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using QuickKit.Blazor.Configuration;
using Scrutor;
using SenacPlataform.Shared.DependencyInjection;
using SenacPlataform.Shared.Enviroment;
using SenacPlataform.Shared.Enviroment.Interfaces;
using SenacPlataform.Shared.Exceptions.ImagemConfig;
using SenacPlataform.Shared.Extensions;
using System.Data;
using System.Reflection;

namespace SenacPlataform.Shared.Config;

public static class SystemConfig
{
    public const string SYSTEM_COOKIE_NAME = "SenacPlataform";
    public const string SYSTEM_NAME = "Plataforma Senac";
    public const int SYSTEM_COOKIE_EXPIRATION = 365;
    public const string ConfiguracaoImagemJsonSection = "ImageConfig";

    #region ASSEMBLY NAMES
    public const string ASSEMBLY_NAME_BANCO_TALENTOS_DOMAIN = "BancoTalentos.Domain";
    public const string ASSEMBLY_NAME_SENAC_PLATAFORM_SHARED = "SenacPlataform.Shared";
    #endregion

    public static IServiceCollection SNConfigureBancoTalentos(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddBlazorSupport();
        configuration.SNConfigureAppSettings();

        services.AddSingleton<IApplicationEnviroment, ApplicationEnviroment>();

        services.SnConfigureDatabase(configuration);

        AddConfiguracaoImagem(services, configuration);

        var assemblyBancoTalentos = Assembly.Load(ASSEMBLY_NAME_BANCO_TALENTOS_DOMAIN);
        var assemblySenacPlataform = Assembly.Load(ASSEMBLY_NAME_SENAC_PLATAFORM_SHARED);

        services.Scan(scan =>
        {
            var selector = scan.FromAssemblies([assemblyBancoTalentos, assemblySenacPlataform])
                                .SNApplyFilter(services);
        });

        //_ = services.AddExceptionHandler<GlobalExceptionHandler>();

        _ = services.AddValidatorsFromAssembly(assemblyBancoTalentos, includeInternalTypes: true);

        _ = services.SNScan(assemblyBancoTalentos, assemblySenacPlataform);

        return services;
    }

    public static IImplementationTypeSelector SNApplyFilter(this IImplementationTypeSelector selector, IServiceCollection services)
    {
        selector
     .AddClasses(classes =>
         classes.Where(c =>
             (c.Name.EndsWith("Service", StringComparison.InvariantCultureIgnoreCase) ||
              c.Name.EndsWith("Repository", StringComparison.InvariantCultureIgnoreCase)) &&
              !services.Any(s => s.ServiceType.IsAssignableFrom(c) || s.ServiceType == c)), false) // Verifica se o tipo já foi registrado ou se é um tipo atribuível
     .AsMatchingInterface()
     .AsImplementedInterfaces()
     .WithTransientLifetime();

        return selector;
    }

    private static IServiceCollection SnConfigureDatabase(this IServiceCollection services, ConfigurationManager configuration)
    {
        return services.AddScoped<IDbConnection>(x => new MySqlConnection(configuration.SNGetConnectionString()));
    }

    /// <summary>
    /// Adiciona a configuração de imagem no container de injeção de dependência.
    /// <para/>
    /// A configuração de imagem é obtida do arquivo appsettings.json.
    /// <para/>
    /// A aplicação não sobe se a configuração não for encontrada ou for inválida.
    /// <para/>
    /// Essa configuração adiciona a instancia em formato scoped. Permitindo o uso da classe <see cref="ImageConfig"/> para acessar as configurações de imagem.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="builder"></param>
    /// <exception cref="ImageConfigurationExceptions">Caso não seja encontrada a seção de configuração.</exception>
    /// <exception cref="ImageConfigurationInvalidException">Caso alguma das configurações seja inválida. </exception>
    private static void AddConfiguracaoImagem(IServiceCollection services, ConfigurationManager builder)
    {
        var imageConfig = builder.GetSection(ConfiguracaoImagemJsonSection).Get<ImageConfig>()
            ?? throw new ImageConfigurationExceptions(ConfiguracaoImagemJsonSection, $"Exceção lançada em {nameof(SystemConfig)}.cs");

        VerificarConfiguraçãoImagem(imageConfig);
        services.AddScoped(x => imageConfig);
    }

    private static void VerificarConfiguraçãoImagem(ImageConfig imageConfig)
    {
        var validationResult = ImageConfig.Validate(imageConfig);

        if (validationResult.IsFailed)
        {
            throw new ImageConfigurationInvalidException(ConfiguracaoImagemJsonSection, "Verificado os dados do objeto.", validationResult.ToErros());
        }
    }
}
