namespace SenacPlataform.Shared.Exceptions;

public class ImageNotFoundException : Exception
{
    public ImageNotFoundException(string? message, string? fileName)
    {
        Message = message;
        FileName = fileName;
    }

    public string? Message { get; }
    public string? FileName { get; }
}