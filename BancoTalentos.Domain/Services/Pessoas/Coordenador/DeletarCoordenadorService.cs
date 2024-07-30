﻿using BancoTalentos.Domain.Repositories.Contracts.Interfaces;
using BancoTalentos.Domain.Services.Pessoas.Base;
using BancoTalentos.Domain.Services.Pessoas.Coordenador.Interfaces;
using FluentResults;

namespace BancoTalentos.Domain.Services.Pessoas.Coordenador;

internal class DeletarCoordenadorService(IPESSOAS_REPOSITORY pessoas_repository,
                                         IPESSOAS_CONTATOS_REPOSITORY pessoas_contatos_repository,
                                         IPESSOAS_HABILIDADES_DISCIPLINAS_REPOSITORY pessoas_habilidades_disciplinas_repository)
    : DeletarPessoaServiceBase(pessoas_repository,
                                pessoas_contatos_repository,
                                pessoas_habilidades_disciplinas_repository), IDeletarCoordenadorService
{
    public async Task<Result> DeletarCoordenadorAsync(int id, CancellationToken cancellationToken = default)
    {
        return await DeletarCoordenadorAsync(id, cancellationToken);
    }
}