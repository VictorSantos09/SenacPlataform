using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SenacPlataform.Shared.Config;

namespace BancoTalentos.Domain.Config;

public static class BancoTalentosDomainConfig
{
    public static IServiceCollection AddBancoTalentos(this IServiceCollection services)
    {
        services.Scan(services =>
        {
            var selector = services.FromAssemblies(typeof(BancoTalentosDomainConfig).Assembly)
            .AddClasses(x => x.Where(classes => classes.Name.EndsWith("Service")
            || classes.Name.EndsWith("Repository")), false)
                    .AsMatchingInterface()
                    .AsImplementedInterfaces()
                    .WithTransientLifetime();
        });

        return services;
    }
}
