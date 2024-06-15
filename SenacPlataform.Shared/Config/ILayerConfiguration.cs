using Microsoft.Extensions.DependencyInjection;

namespace SenacPlataform.Shared.Config;

public interface ILayerConfiguration
{
    IServiceCollection Configure(IServiceCollection services);
}
