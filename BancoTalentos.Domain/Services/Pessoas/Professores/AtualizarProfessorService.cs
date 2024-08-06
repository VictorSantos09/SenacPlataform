using BancoTalentos.Domain.Repositories.Contracts.Interfaces;
using BancoTalentos.Domain.Services.Imagem;
using BancoTalentos.Domain.Services.Pessoas.Base;
using BancoTalentos.Domain.Services.Pessoas.Base.Dto;
using BancoTalentos.Domain.Services.Pessoas.Professores.Interfaces;
using FluentResults;
using Microsoft.AspNetCore.Http;

namespace BancoTalentos.Domain.Services.Pessoas.Professores;

internal class AtualizarProfessorService(IPESSOAS_REPOSITORY pessoas_repository,
                                 IPESSOAS_CONTATOS_REPOSITORY pessoas_contatos_repository,
                                 IPESSOAS_HABILIDADES_DISCIPLINAS_REPOSITORY pessoas_habilidades_disciplinas_repository,
                                 IImagemService imagemService)
    : AtualizarPessoaServiceBase(pessoas_repository, pessoas_contatos_repository, pessoas_habilidades_disciplinas_repository, imagemService), IAtualizarProfessorService
{
    public async Task<Result> AtualizarAsync(ProfessorDto dto, CancellationToken cancellationToken = default)
    {
        return await AtualizarPessoaAsync(dto, cancellationToken);
    }

    public async Task<Result> AtualizarAsync(IFormFile fotoPerfil, int id, CancellationToken cancellationToken = default)
    {
        return await AtualizarPessoaFotoPerfilAsync(fotoPerfil, id, cancellationToken);
    }
}
