using BancoTalentos.Domain.Config;
using BancoTalentos.Domain.Exceptions;
using FluentValidation;
using SenacPlataform.Shared.DependencyInjection;
using SenacPlataform.Shared.Extensions;

namespace BancoTalentos.API.Config;

internal static class BancoTalentosConfig
{
    public const string ConfiguracaoImagemJsonSection = "ImageConfig";
    public static IServiceCollection AddBancoTalentos(this IServiceCollection services, WebApplicationBuilder builder)
    {
        AddConfiguracaoImagem(services, builder);
        _ = services.AddValidatorsFromAssembly(typeof(BancoTalentosDomainConfig).Assembly, includeInternalTypes: true);
        _ = services.AddDependencies(typeof(BancoTalentosDomainConfig).Assembly);
        _ = services.AddDependencies(typeof(BancoTalentosConfig).Assembly);
        return services;
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
    /// <exception cref="ImageConfigurationNotFoundException">Caso não seja encontrada a seção de configuração.</exception>
    /// <exception cref="ImageConfigurationInvalidException">Caso alguma das configurações seja inválida. </exception>
    private static void AddConfiguracaoImagem(IServiceCollection services, WebApplicationBuilder builder)
    {
        var imageConfig = builder.Configuration.GetSection(ConfiguracaoImagemJsonSection).Get<ImageConfig>()
            ?? throw new ImageConfigurationNotFoundException(ConfiguracaoImagemJsonSection, $"Exceção lançada em {nameof(BancoTalentosConfig)}.cs");

        CheckImageConfiguration(imageConfig);
        services.AddScoped(x => imageConfig);
    }

    private static void CheckImageConfiguration(ImageConfig imageConfig)
    {
        var validationResult = ImageConfig.Validate(imageConfig);

        if(validationResult.IsFailed)
        {
            throw new ImageConfigurationInvalidException(ConfiguracaoImagemJsonSection, "Verificado os dados do objeto.", validationResult.ToErros());
        }
    }
}
