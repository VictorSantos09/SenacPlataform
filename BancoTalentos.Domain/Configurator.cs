using Microsoft.Extensions.DependencyInjection;

namespace BancoTalentos.Domain;

public static class Configurator
{
    public static IServiceCollection SNAddBancoTalentosDomain(this IServiceCollection collection)
    {
        collection.Scan(services =>
        {
            var selector = services.FromAssemblies(typeof(Configurator).Assembly)
            .AddClasses(x => x.Where(classes => classes.Name.EndsWith("Service")
            || classes.Name.Contains("REPOSITORY")), false)
                    .AsMatchingInterface()
                    .AsImplementedInterfaces()
                    .WithTransientLifetime();
        });

        return collection;
    }
}
