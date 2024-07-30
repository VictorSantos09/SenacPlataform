using BancoTalentos.Domain.Entity.Enums;

namespace BancoTalentos.Domain.Services.Pessoas.Base.Dto;

public record CoordenadorDto : PessoaDto
{
    internal override CARGO Cargo { get; init; } = CARGO.COORDENADOR;
}
