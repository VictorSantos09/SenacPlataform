using Microsoft.Extensions.DependencyInjection;
using SenacPlataform.Shared.DependencyInjection;
using System.Reflection;

namespace SenacPlataform.Shared.Config;

public sealed class LayerConfiguration
{
    public static void Apply(IServiceCollection services, bool publicOnly = false, params Assembly[] assemblies)
    {
        _ = AssemblyScanner.ScanFromAssemblies(services, publicOnly, assemblies);
    }
}
