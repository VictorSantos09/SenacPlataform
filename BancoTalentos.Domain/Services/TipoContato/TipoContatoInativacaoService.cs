using BancoTalentos.Domain.Repositories.Contracts.Interfaces;
using BancoTalentos.Domain.Services.TipoContato.Base;
using BancoTalentos.Domain.Services.TipoContato.Interface;
using FluentResults;

namespace BancoTalentos.Domain.Services.TipoContato;
internal class TipoContatoInativacaoService : TipoContatoServiceBase, ITipoContatoInativacaoService
{
    private readonly ITIPOS_CONTATOS_REPOSITORY _tipos_contatos_repository;

    public TipoContatoInativacaoService(ITIPOS_CONTATOS_REPOSITORY tipos_contatos_repository, IPESSOAS_CONTATOS_REPOSITORY pessoas_contatos_repository) : base(pessoas_contatos_repository)
    {
        _tipos_contatos_repository = tipos_contatos_repository;
    }

    public async Task<Result> AlterarAtivacaoAsync(int id, bool ativar, CancellationToken cancellationToken)
    {
        try
        {
            _tipos_contatos_repository.BeginTransaction();

            var podeInativar = await NaoTemPessoaAtreladaAsync(id, cancellationToken);

            if (podeInativar.IsFailed)
            {
                _tipos_contatos_repository.Rollback();
                return podeInativar;
            }

            var tipoContato = await _tipos_contatos_repository.GetByIdAsync(id, cancellationToken);

            if (tipoContato is null)
            {
                _tipos_contatos_repository.Rollback();
                return Result.Fail(TIPO_CONTATO_NAO_ENCONTRADO);
            }


            tipoContato.DATA_INATIVACAO = ativar ? null : DateTime.Now;

            var atualizado = await _tipos_contatos_repository.UpdateAsync(tipoContato, cancellationToken);

            if (atualizado == 0)
            {
                _tipos_contatos_repository.Rollback();
                return Result.Fail("Erro ao alteração a ativação do tipo de contato.");
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
