namespace BancoTalentos.Domain.Services.Imagem.Dto;

public record ImagemBase64DTO
{
    public string Image { get; set; }
    public long Size { get; set; }
    public string ContentType { get; set; }
    public string FileName { get; set; }
}