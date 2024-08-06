namespace BancoTalentos.Domain.Exceptions.ImagemConfig;

public class ImagemNaoInformadaException : ApplicationException
{
    public ImagemNaoInformadaException(string? message) : base(message)
    {
    }
}
