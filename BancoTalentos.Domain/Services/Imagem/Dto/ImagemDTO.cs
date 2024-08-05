namespace BancoTalentos.Domain.Services.Imagem.Dto;

public record ImagemDTO
{
    public string MimeType { get; set; }
    public MemoryStream ImagemMemory { get; set; }

    public ImagemDTO(string defaultMimeType, MemoryStream imagemMemory)
    {
        MimeType = defaultMimeType;
        ImagemMemory = imagemMemory;
    }
}
