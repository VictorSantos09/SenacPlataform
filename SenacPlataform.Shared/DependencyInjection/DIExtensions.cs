using Microsoft.Extensions.DependencyInjection;
using SenacPlataform.Shared.Config;
using System.Reflection;

namespace SenacPlataform.Shared.DependencyInjection;

public static class DIExtensions
{
    public static IServiceCollection SNScan(this IServiceCollection services, params Assembly[] assemblies)
    {
        services.Scan(scan => scan.FromAssemblies(assemblies)
                .SNApplyFilter(services));

        return services;
    }
}
