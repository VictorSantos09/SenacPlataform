using BancoTalentos.Domain.Config;
using SenacPlataform.Shared.DependencyInjection;

namespace BancoTalentos.API.Config;

internal static class BancoTalentosConfig
{
    public static IServiceCollection AddBancoTalentos(this IServiceCollection services)
    {
        _ = services.AddDependencies(typeof(BancoTalentosConfig).Assembly);
        _ = services.AddDependencies(typeof(BancoTalentosDomainConfig).Assembly);
        return services;
    }
}
