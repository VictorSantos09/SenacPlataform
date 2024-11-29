using BancoTalentos.Domain.Repositories.Contracts.Interfaces;
using FluentResults;

namespace BancoTalentos.Domain.Services.TipoContato.Base;
internal abstract class TipoContatoServiceBase
{
    private readonly IPESSOAS_CONTATOS_REPOSITORY _pessoas_contatos_repository;
    protected const string TIPO_CONTATO_NAO_ENCONTRADO = "Tipo de contato não encontrado.";

    protected TipoContatoServiceBase(IPESSOAS_CONTATOS_REPOSITORY pessoas_contatos_repository)
    {
        _pessoas_contatos_repository = pessoas_contatos_repository;
    }

    public async Task<Result> NaoTemPessoaAtreladaAsync(int id, CancellationToken cancellationToken)
    {
        if (await _pessoas_contatos_repository.HasPessoaComTipoContatoAsync(id, cancellationToken))
        {
            return Result.Fail($"Existem pessoas cadastradas com o tipo de contato.");
        }

        return Result.Ok();
    }
}
