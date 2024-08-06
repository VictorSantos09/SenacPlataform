using System.Configuration;

namespace BancoTalentos.Domain.Exceptions.ImagemConfig;

internal class ImagemErroAtualizarException : ApplicationException
{
    public string ImageAntigaFileName { get; init; }
    public string? ImagemNovaFileName { get; init; }
    public ImagemErroAtualizarException(string? message, string imageAntigaFileName, string? imagemNovaFileName = null) : base(message)
    {
        ImageAntigaFileName = imageAntigaFileName;
        ImagemNovaFileName = imagemNovaFileName;
    }
}
