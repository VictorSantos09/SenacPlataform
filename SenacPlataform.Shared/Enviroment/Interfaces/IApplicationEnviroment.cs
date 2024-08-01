namespace SenacPlataform.Shared.Enviroment.Interfaces;

public interface IApplicationEnviroment
{
    bool IsDevelopment();
    bool IsProduction();
}