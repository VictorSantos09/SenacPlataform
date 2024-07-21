using BancoTalentos.Domain.Entity.Enums;
using BancoTalentos.Domain.Services.Contato.Dto;

namespace BancoTalentos.Domain.Services.Pessoas.Professores.Dto;

public record ProfessorDto : PessoaDto
{
    internal override CARGO Cargo { get; init; } = CARGO.PROFESSOR;
}

public record CoordenadorDto : PessoaDto
{
    internal override CARGO Cargo { get; init; } = CARGO.COORDENADOR;
}

public abstract record PessoaDto
{
    public required string Nome { get; set; }
    public required byte[]? Foto { get; set; }
    internal abstract CARGO Cargo { get; init; }
    public required int CargaHorariaSemanal { get; set; }
    public required IEnumerable<ContatoDto> Contatos { get; set; }
    public required int Id { get; set; }
    public required IEnumerable<int> IdsDisciplinas { get; set; }
}
