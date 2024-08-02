namespace BancoTalentos.Domain.Services.Pessoas.Base.Dto;

public record PessoaImagemDto
{
    public MemoryStream MemoryStreamImagem { get; set; }
    public string ImageType { get; set; }

    public PessoaImagemDto(MemoryStream memoryStreamImagem, string imageType)
    {
        MemoryStreamImagem = memoryStreamImagem;
        ImageType = imageType;
    }
}
