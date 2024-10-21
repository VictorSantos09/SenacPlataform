using BancoTalentos.Domain.Entity.Enums;

namespace BancoTalentos.UI.Components.Pages.Pessoas.dto;

public record PessoaDto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string? Foto { get; set; }
    public CARGO Cargo { get; set; }
    public int CargaHoraria { get; set; }
    public IEnumerable<ContatoDto> Contatos { get; set; }
    public IEnumerable<HabilidadeDisciplinaDto> HabilidadesDisciplinas { get; set; }
}

public record ContatoDto
{
    public int MyProperty { get; set; }
}

public record class HabilidadeDisciplinaDto
{
    public int MyProperty { get; set; }
}
