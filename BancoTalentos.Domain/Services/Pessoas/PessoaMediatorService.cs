using BancoTalentos.Domain.Entity.Enums;
using BancoTalentos.Domain.Services.Pessoas.Base.Dto;
using BancoTalentos.Domain.Services.Pessoas.Coordenador.Interfaces;
using BancoTalentos.Domain.Services.Pessoas.Professores.Interfaces;
using FluentResults;

namespace BancoTalentos.Domain.Services.Pessoas;

internal class PessoaMediatorService(
    ICadastrarCoordenadorService cadastrarCoordenadorService,
    ICadastrarProfessorService cadastrarProfessorService, 
    IDeletarCoordenadorService deletarCoordenadorService,
    IDeletarProfessorService deletarProfessorService,
    IAtualizarProfessorService atualizarProfessorService,
    IAtualizarCoordenadorService atualizarCoordenadorService) : IPessoaMediatorService
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

    public async Task<Result> AtualizarAsync(PessoaDto dto, CancellationToken cancellationToken = default)
    {
        switch (dto.Cargo)
        {
            case CARGO.PROFESSOR:
                return await atualizarProfessorService.AtualizarAsync(dto.ToProfessor(), cancellationToken);
            case CARGO.COORDENADOR:
                return await atualizarCoordenadorService.AtualizarCoordenadorAsync(dto.ToCoodenador(), cancellationToken);
            default:
                throw new ArgumentException("Cargo informado é inválido");
        }
    }

    public async Task<Result> DeletarAsync(int id, CARGO cargo, CancellationToken cancellationToken = default)
    {
        switch (cargo)
        {
            case CARGO.PROFESSOR:
                return await deletarProfessorService.DeletarAsync(id, cancellationToken);
            case CARGO.COORDENADOR:
                return await deletarCoordenadorService.DeletarAsync(id, cancellationToken);
            default:
                throw new ArgumentException("Cargo informado é inválido");
        }
    }
}
