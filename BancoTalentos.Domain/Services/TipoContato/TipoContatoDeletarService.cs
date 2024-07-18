using BancoTalentos.Domain.Repositories.Contracts.Interfaces;
using BancoTalentos.Domain.Services.TipoContato.Base;
using BancoTalentos.Domain.Services.TipoContato.Interface;
using FluentResults;

namespace BancoTalentos.Domain.Services.TipoContato;
internal class TipoContatoDeletarService : TipoContatoServiceBase, ITipoContatoDeletarService
{
    private readonly ITIPOS_CONTATOS_REPOSITORY _tipos_contatos_repository;

    public TipoContatoDeletarService(ITIPOS_CONTATOS_REPOSITORY tipos_contatos_repository,
                                     IPESSOAS_CONTATOS_REPOSITORY pessoas_contatos_repository) : base(pessoas_contatos_repository)
    {
        _tipos_contatos_repository = tipos_contatos_repository;
    }

    public async Task<Result> DeletarAsync(int id, CancellationToken cancellationToken)
    {
        try
        {
            _tipos_contatos_repository.BeginTransaction();

            var naoTemPessoaAtrelada = await NaoTemPessoaAtreladaAsync(id, cancellationToken);

            if (naoTemPessoaAtrelada.IsFailed)
            {
                _tipos_contatos_repository.Rollback();
                return naoTemPessoaAtrelada;
            }

            var tipoContato = await _tipos_contatos_repository.GetByIdAsync(id, cancellationToken);

            if (tipoContato is null)
            {
                _tipos_contatos_repository.Rollback();
                return Result.Fail(TIPO_CONTATO_NAO_ENCONTRADO);
            }

            var deletado = await _tipos_contatos_repository.DeleteAsync(tipoContato, cancellationToken);

            if (deletado == 0)
            {
                _tipos_contatos_repository.Rollback();
                return Result.Fail("Erro ao deletar tipo de contato.");
            }

            _tipos_contatos_repository.Commit();

            return Result.Ok();
        }
        catch (Exception)
        {
            _tipos_contatos_repository.Rollback();
            throw;
        }
    }
}
