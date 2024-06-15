using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace SenacPlataform.Shared.DependencyInjection;

public static class DIExtensions
{
    public static IServiceCollection AddDependencies(this IServiceCollection services, Assembly assembly)
    {
        services.Scan(scan => scan.FromAssemblies(assembly)
       .AddClasses(classes => classes.Where(c => c.Name.ToUpper().EndsWith("REPOSITORY") || c.Name.ToUpper().EndsWith("SERVICE")), false)
       .AsImplementedInterfaces()
       .WithTransientLifetime());

        return services;
    }
}
