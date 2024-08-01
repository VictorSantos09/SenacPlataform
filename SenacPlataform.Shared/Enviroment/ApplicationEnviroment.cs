using Microsoft.Extensions.Hosting;
using SenacPlataform.Shared.Enviroment.Interfaces;

namespace SenacPlataform.Shared.Enviroment;

internal class ApplicationEnviroment(IHostEnvironment hostEnvironment) : IApplicationEnviroment
{
    public bool IsDevelopment()
    {
        return hostEnvironment.IsDevelopment();
    }

    public bool IsProduction()
    {
        return hostEnvironment.IsProduction();
    }
}
