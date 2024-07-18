using BancoTalentos.Domain.Entity;
using BancoTalentos.Domain.Repositories.Contracts.Interfaces;
using BancoTalentos.Domain.Services.TipoContato.Interface;
using FluentResults;
using FluentValidation;
using SenacPlataform.Shared.Extensions;

namespace BancoTalentos.Domain.Services.TipoContato;
internal class TipoContatoAtualizarService : ITipoContatoAtualizarService
{
    private readonly ITIPOS_CONTATOS_REPOSITORY _tipos_contatos_repository;
    private readonly IValidator<TIPOS_CONTATOS> _validator;

    public TipoContatoAtualizarService(ITIPOS_CONTATOS_REPOSITORY tipos_contatos_repository,
                                       IValidator<TIPOS_CONTATOS> validator)
    {
        _tipos_contatos_repository = tipos_contatos_repository;
        _validator = validator;
    }

    public async Task<Result> AtualizarAsync(int id, string tipo, CancellationToken cancellationToken)
    {
        if (tipo.IsEmpty())
        {
            return Result.Fail("Tipo de contato não informado.");
        }

        var tipoContato = await _tipos_contatos_repository.GetByIdAsync(id, cancellationToken);

        if (tipoContato is null)
        {
            return Result.Fail($"Tipo de contato com código {id} não encontrado.");
        }

        tipoContato.TIPO = tipo;

        var validationResult = await _validator.ValidateAsync(tipoContato, cancellationToken);

        if (!validationResult.IsValid)
        {
            return validationResult.ToErrorResult();
        }

        try
        {
            _tipos_contatos_repository.BeginTransaction();

            if (await _tipos_contatos_repository.ExistsBy_IDX_TIPOS_CONTATOS_002_Async(tipo, cancellationToken))
            {
                _tipos_contatos_repository.Rollback();
                return Result.Fail("Tipo de contato já cadastrado.");
            }

            var result = await _tipos_contatos_repository.UpdateAsync(tipoContato, cancellationToken);

            if (result == 0)
            {
                _tipos_contatos_repository.Rollback();
                return Result.Fail("Erro ao atualizar o tipo de contato.");
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
