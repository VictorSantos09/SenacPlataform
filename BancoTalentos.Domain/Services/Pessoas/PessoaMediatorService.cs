using BancoTalentos.Domain.Entity.Enums;
using BancoTalentos.Domain.Services.Pessoas.Base.Dto;
using BancoTalentos.Domain.Services.Pessoas.Coordenador.Interfaces;
using BancoTalentos.Domain.Services.Pessoas.Professores.Interfaces;
using FluentResults;

namespace BancoTalentos.Domain.Services.Pessoas;

internal class PessoaMediatorService(ICadastrarCoordenadorService cadastrarCoordenadorService, ICadastrarProfessorService cadastrarProfessorService) : IPessoaMediatorService
{
    public async Task<Result> CadastrarAsync(PessoaDto dto, CancellationToken cancellationToken = default)
    {
        switch (dto.Cargo)
        {
            case CARGO.PROFESSOR:
                return await cadastrarProfessorService.CadastrarAsync(dto.ToProfessor(), cancellationToken);
            case CARGO.COORDENADOR:
                return await cadastrarCoordenadorService.CadastrarAsync(dto.ToCoodenador(), cancellationToken);
            default:
                throw new ArgumentException("Cargo informado é inválido");
        }
    }
}
