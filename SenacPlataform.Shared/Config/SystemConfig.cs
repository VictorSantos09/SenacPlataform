using Microsoft.Extensions.DependencyInjection;

namespace SenacPlataform.Shared.Config;

public static class SystemConfig
{
    public const string SYSTEM_COOKIE_NAME = "SenacPlataform";
    public const string SYSTEM_NAME = "Plataforma Senac";
    public const int SYSTEM_COOKIE_EXPIRATION = 365;
    public static IServiceCollection AddBancoTalentos(this IServiceCollection services)
    {
        services.Scan(services =>
        {
            var selector = services.FromAssemblies(typeof(SystemConfig).Assembly)
            .AddClasses(x => x.Where(classes => classes.Name.EndsWith("Service")
            || classes.Name.EndsWith("Repository")), false)
                    .AsMatchingInterface()
                    .AsImplementedInterfaces()
                    .WithTransientLifetime();
        });

        return services;
    }
}
