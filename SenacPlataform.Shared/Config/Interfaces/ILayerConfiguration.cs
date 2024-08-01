using Microsoft.Extensions.DependencyInjection;

namespace SenacPlataform.Shared.Config.Interfaces;

public interface ILayerConfiguration
{
    IServiceCollection Configure(IServiceCollection services);
}
