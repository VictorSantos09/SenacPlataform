using BancoTalentos.Domain.Entity;
using BancoTalentos.Domain.Repositories.Contracts.Interfaces;
using BancoTalentos.Domain.Services.Disciplina.Dto;
using BancoTalentos.Domain.Services.Disciplina.Interfaces;
using FluentResults;
using FluentValidation;
using SenacPlataform.Shared.Extensions;

namespace BancoTalentos.Domain.Services.Disciplina;
internal class AtualizarDisciplinaService : IAtualizarDisciplinaService
{
    private readonly IDISCIPLINAS_REPOSITORY _disciplinas_repository;
    private readonly IValidator<DISCIPLINAS> _validator;

    public AtualizarDisciplinaService(IDISCIPLINAS_REPOSITORY disciplinas_repository,
                                      IValidator<DISCIPLINAS> validator)
    {
        _disciplinas_repository = disciplinas_repository;
        _validator = validator;
    }

    public async Task<Result> AtualizarAsync(DisciplinaDto dto, CancellationToken cancellationToken)
    {
        if (dto.Id.IsNotValidIdentifier())
        {
            return Result.Fail("Código da disciplina é inválido.");
        }

        var disciplina = await _disciplinas_repository.GetByIdAsync(dto.Id, cancellationToken);

        if (disciplina is null)
        {
            return Result.Fail("Disciplina não encontrada.");
        }

        if (disciplina.NOME != dto.Nome && await _disciplinas_repository.ExistsBy_IDX_DISCIPLINAS_001_Async(dto.Nome, cancellationToken))
        {
            return Result.Fail("Nome da disciplina não pode ser alterado. Pois já existe outra com esse nome.");
        }

        disciplina.NOME = dto.Nome;
        disciplina.DESCRICAO = dto.Descricao;
        disciplina.CARGA_HORARIA = dto.CargaHoraria;

        var validationResult = await _validator.ValidateAsync(disciplina, cancellationToken);

        if (validationResult.IsInvalid())
        {
            return validationResult.ToErrorResult();
        }

        try
        {
            _disciplinas_repository.BeginTransaction();

            var result = await _disciplinas_repository.UpdateAsync(disciplina, cancellationToken);

            if (result == 0)
            {
                _disciplinas_repository.Rollback();
                return Result.Fail("Não foi possível atualizar a disciplina.");
            }

            _disciplinas_repository.Commit();
            return Result.Ok();
        }
        catch (Exception)
        {
            _disciplinas_repository.Rollback();
            throw;
        }
    }
}
