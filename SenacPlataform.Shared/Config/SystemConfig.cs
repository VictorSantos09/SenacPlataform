using Microsoft.Extensions.DependencyInjection;
using Scrutor;

namespace SenacPlataform.Shared.Config;

public static class SystemConfig
{
    public const string SYSTEM_COOKIE_NAME = "SenacPlataform";
    public const string SYSTEM_NAME = "Plataforma Senac";
    public const int SYSTEM_COOKIE_EXPIRATION = 365;
    
    public static IServiceCollection SNAddBancoTalentosShared(this IServiceCollection services)
    {
        services.Scan(services =>
        {
            var selector = services.FromAssemblies(typeof(SystemConfig).Assembly)
            .ApplyFilter();
        });

        return services;
    }

    public static IServiceCollection SNAddBancoTalentos<T>(this IServiceCollection services)
    {
        services.Scan(services =>
        {
            var selector = services.FromAssemblyOf<T>()
            .ApplyFilter();
        });

        return services;
    }

    private static IImplementationTypeSelector ApplyFilter(this IImplementationTypeSelector selector)
    {
            selector
            .AddClasses(x => x.Where(classes => classes.Name.EndsWith("Service", StringComparison.InvariantCultureIgnoreCase)
            || classes.Name.EndsWith("Repository", StringComparison.InvariantCultureIgnoreCase)), false)
                    .AsMatchingInterface()
                    .AsImplementedInterfaces()
                    .WithTransientLifetime();

        return selector;
    }
}
