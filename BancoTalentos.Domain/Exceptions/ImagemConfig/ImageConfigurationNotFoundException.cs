namespace BancoTalentos.Domain.Exceptions.ImagemConfig;
public class ImageConfigurationNotFoundException(string sectionBuscada, string message) : ApplicationException(CreateMessage(sectionBuscada, message))
{
    public static string CreateMessage(string sectionBuscada, string? details)
    {
        return $"A configuração de imagem não foi encontrada na aplicação. Verifique se existe a seção {sectionBuscada} no arquivo appsettings.json." +
            $"\n Seção buscada: {sectionBuscada}" +
            $"\n Detalhes: {details ?? "Não fornecidos."}";
    }
}

public class ImageConfigurationInvalidException(string sectionBuscada, string message, object? data = null) : ApplicationException(CreateMessage(sectionBuscada, message, data))
{
    public static string CreateMessage(string sectionBuscada, string? details, object? data = null)
    {
        return $"A configuração de imagem encontrada na aplicação é inválida. Verifique se a seção {sectionBuscada} no arquivo appsettings.json está correta." +
            $"\n Seção buscada: {sectionBuscada}" +
            $"\n Detalhes: {details ?? "Não fornecidos." +
            $"\n Data Info Object: {data}"}";
    }
}