namespace SenacPlataform.Shared.Exceptions.ImagemConfig;

public class ImagemErroAtualizarException : ApplicationException
{
    public string ImageAntigaFileName { get; init; }
    public string? ImagemNovaFileName { get; init; }
    public ImagemErroAtualizarException(string? message, string imageAntigaFileName, string? imagemNovaFileName = null) : base(message)
    {
        ImageAntigaFileName = imageAntigaFileName;
        ImagemNovaFileName = imagemNovaFileName;
    }
}
