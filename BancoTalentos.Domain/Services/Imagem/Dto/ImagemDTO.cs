namespace BancoTalentos.Domain.Services.Imagem.Dto;

public record ImagemDTO
{
    public string MimeType { get; set; }
    public string Imagem { get; set; }

    public ImagemDTO(string defaultMimeType, string imagemMemory)
    {
        MimeType = defaultMimeType;
        Imagem = imagemMemory;
    }
}
