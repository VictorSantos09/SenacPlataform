using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SenacPlataform.Shared.Config;
using SenacPlataform.Shared.DependencyInjection;
using SenacPlataform.Shared.Exceptions.ImagemConfig;
using SenacPlataform.Shared.Extensions;

namespace BancoTalentos.Domain.Config;

public static class BancoTalentosConfig
{
    public const string ConfiguracaoImagemJsonSection = "ImageConfig";
    public static IServiceCollection AddBancoTalentosConfig(this IServiceCollection services, IConfiguration builder)
    {
        //_ = services.AddExceptionHandler<GlobalExceptionHandler>();
        AddConfiguracaoImagem(services, builder);
        _ = services.AddValidatorsFromAssembly(typeof(BancoTalentosConfig).Assembly, includeInternalTypes: true);
        _ = services.AddDependencies(typeof(BancoTalentosConfig).Assembly);
        _ = services.AddDependencies(typeof(BancoTalentosConfig).Assembly);

        _ = services.AddValidatorsFromAssembly(typeof(SystemConfig).Assembly, includeInternalTypes: true);
        _ = services.AddDependencies(typeof(SystemConfig).Assembly);
        _ = services.AddDependencies(typeof(SystemConfig).Assembly);
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
    /// <exception cref="ImageConfigurationExceptions">Caso não seja encontrada a seção de configuração.</exception>
    /// <exception cref="ImageConfigurationInvalidException">Caso alguma das configurações seja inválida. </exception>
    private static void AddConfiguracaoImagem(IServiceCollection services, IConfiguration builder)
    {
        var imageConfig = builder.GetSection(ConfiguracaoImagemJsonSection).Get<ImageConfig>()
            ?? throw new ImageConfigurationExceptions(ConfiguracaoImagemJsonSection, $"Exceção lançada em {nameof(BancoTalentosConfig)}.cs");

        CheckImageConfiguration(imageConfig);
        services.AddScoped(x => imageConfig);
    }

    private static void CheckImageConfiguration(ImageConfig imageConfig)
    {
        var validationResult = ImageConfig.Validate(imageConfig);

        if (validationResult.IsFailed)
        {
            throw new ImageConfigurationInvalidException(ConfiguracaoImagemJsonSection, "Verificado os dados do objeto.", validationResult.ToErros());
        }
    }
}
