namespace BancoTalentos.Domain.Exceptions;

public class ImagemNaoInformadaException : ApplicationException
{
    public ImagemNaoInformadaException(string? message) : base(message)
    {
    }
}
