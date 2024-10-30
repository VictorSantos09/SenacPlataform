using BancoTalentos.Domain.Entity.Enums;

namespace BancoTalentos.Domain.Services.Pessoas.Base.Dto;

public record ProfessorDto : PessoaDto
{
    public override CARGO Cargo { get; init; } = CARGO.PROFESSOR;
}
