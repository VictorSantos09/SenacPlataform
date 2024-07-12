using BancoTalentos.Domain.Entity.Enums;
using BancoTalentos.Domain.Services.Contato.Dto;

namespace BancoTalentos.Domain.Services.Professores.Dto;

public sealed record ProfessorDto
{
    public string Nome { get; set; }
    public byte[]? Foto { get; set; }
    internal CARGO Cargo { get; } = CARGO.PROFESSOR;
    public int CargaHoraria { get; set; }
    public IEnumerable<int> IdsDisciplinas { get; set; }
    public IEnumerable<ContatoDto> Contatos { get; set; }
    public int Id { get; set; }
}
