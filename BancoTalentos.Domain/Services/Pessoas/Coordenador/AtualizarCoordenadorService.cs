using BancoTalentos.Domain.Repositories.Contracts.Interfaces;
using BancoTalentos.Domain.Services.Pessoas.Base;
using BancoTalentos.Domain.Services.Pessoas.Professores.Dto;
using FluentResults;
using Microsoft.AspNetCore.Http;

namespace BancoTalentos.Domain.Services.Pessoas.Coordenador;

internal class AtualizarCoordenadorService(IPESSOAS_REPOSITORY pessoas_repository,
                                 IPESSOAS_CONTATOS_REPOSITORY pessoas_contatos_repository,
                                 IPESSOAS_HABILIDADES_DISCIPLINAS_REPOSITORY pessoas_habilidades_disciplinas_repository)
    : AtualizarPessoaServiceBase(pessoas_repository, pessoas_contatos_repository, pessoas_habilidades_disciplinas_repository), IAtualizarCoordenadorService
{
    public async Task<Result> AtualizarAsync(CoordenadorDto dto, CancellationToken cancellationToken = default)
    {
        return await AtualizarAsync(dto, cancellationToken);
    }
    public async Task<Result> AtualizarAsync(IFormFile fotoPerfil, int id, CancellationToken cancellationToken = default)
    {
        return await AtualizarAsync(fotoPerfil, id, cancellationToken);
    }
}
