using BancoTalentos.Domain.Entity.Enums;
using BancoTalentos.Domain.Services.Contato.Dto;

namespace BancoTalentos.Domain.Services.Professores.Dto;

public record ProfessorDto
{
    public required string Nome { get; set; }
    public required byte[]? Foto { get; set; }
    internal CARGO Cargo { get; } = CARGO.PROFESSOR;
    public required int CargaHorariaSemanal { get; set; }
    public required IEnumerable<ContatoDto> Contatos { get; set; }
    public required int Id { get; set; }
    public required IEnumerable<int> IdsDisciplinas { get; set; }
}
