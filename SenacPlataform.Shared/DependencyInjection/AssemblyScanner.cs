using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using System.Reflection;

namespace SenacPlataform.Shared.DependencyInjection;
public class AssemblyScanner
{
    public static IServiceCollection ScanCallingAssembly(IServiceCollection services, bool publicOnly = false)
    {
        services.Scan(services =>
        {
            var selector = services.FromCallingAssembly();
            ApplyDefault(selector, publicOnly);
        });

        return services;
    }

    public static IServiceCollection ScanFromAssemblies(IServiceCollection services, bool publicOnly = false, params Assembly[] assemblies)
    {
        services.Scan(services =>
        {
            var selector = services.FromAssemblies(assemblies);
            ApplyDefault(selector, publicOnly);
        });

        return services;
    }

    public static IServiceCollection ScanFromExecutingAssembly(IServiceCollection services, bool publicOnly = false)
    {
        services.Scan(services =>
        {
            var selector = services.FromExecutingAssembly();
            ApplyDefault(selector, publicOnly);
        });

        return services;
    }

    public static IServiceCollection ScanFromAssemblyOf<TType>(IServiceCollection services, bool publicOnly = false)
    {
        services.Scan(services =>
        {
            IImplementationTypeSelector selector = services.FromAssemblyOf<TType>();
            ApplyDefault(selector, publicOnly);
        });

        return services;
    }

    private static IImplementationTypeSelector ApplyDefault(IImplementationTypeSelector services, bool publicOnly = false)
    {
        return services.AddClasses(x => x.Where(classes => classes.Name.EndsWith("Service")
                                                           || classes.Name.EndsWith("Repository")), publicOnly)
                    .AsMatchingInterface()
                    .AsImplementedInterfaces()
                    .WithTransientLifetime();
    }
}