using BancoTalentos.Domain.Entity.Enums;

namespace BancoTalentos.Domain.Services.Pessoas.Base.Dto;

public record ProfessorDto : PessoaDto
{
    internal override CARGO Cargo { get; init; } = CARGO.PROFESSOR;
}
