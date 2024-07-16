namespace BancoTalentos.Domain.Services.Contato.Dto;

public sealed record ContatoDto
{
    public int IdTipo { get; set; }
    public string Contato { get; set; }
    public int Id { get; set; }
}
