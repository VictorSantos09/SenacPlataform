using BancoTalentos.Domain.Config;
using FluentValidation;
using SenacPlataform.Shared.DependencyInjection;

namespace BancoTalentos.API.Config;

internal static class BancoTalentosConfig
{
    public static IServiceCollection AddBancoTalentos(this IServiceCollection services)
    {
        _ = services.AddValidatorsFromAssembly(typeof(BancoTalentosDomainConfig).Assembly);
        _ = services.AddDependencies(typeof(BancoTalentosDomainConfig).Assembly);
        _ = services.AddDependencies(typeof(BancoTalentosConfig).Assembly);
        return services;
    }
}
