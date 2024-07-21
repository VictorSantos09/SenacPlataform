using BancoTalentos.Domain.Entity;
using BancoTalentos.Domain.Repositories.Contracts.Interfaces;
using BancoTalentos.Domain.Services.Disciplina.Dto;
using BancoTalentos.Domain.Services.Disciplina.Interfaces;
using FluentResults;
using FluentValidation;
using SenacPlataform.Shared.Extensions;

namespace BancoTalentos.Domain.Services.Disciplina;
internal class CadastrarDisciplinaService : ICadastrarDisciplinaService
{
    private readonly IDISCIPLINAS_REPOSITORY _disciplinas_repository;
    private readonly IValidator<DISCIPLINAS> _validator;

    public CadastrarDisciplinaService(IDISCIPLINAS_REPOSITORY disciplinas_repository,
                                      IValidator<DISCIPLINAS> validator)
    {
        _disciplinas_repository = disciplinas_repository;
        _validator = validator;
    }

    public async Task<Result> CadastrarAsync(DisciplinaDto dto, CancellationToken cancellationToken)
    {
        var novaDiscplina = new DISCIPLINAS
        {
            NOME = dto.Nome,
            DESCRICAO = dto.Descricao,
            CARGA_HORARIA = dto.CargaHoraria
        };

        var validationResult = await _validator.ValidateAsync(novaDiscplina, cancellationToken);

        if (!validationResult.IsValid)
        {
            return validationResult.ToErrorResult();
        }

        try
        {
            _disciplinas_repository.BeginTransaction();

            var disciplinaExiste = await _disciplinas_repository.ExistsBy_IDX_DISCIPLINAS_001_Async(dto.Nome, cancellationToken);

            if (disciplinaExiste)
            {
                return Result.Fail("Disciplina já cadastrada.");
            }

            var result = await _disciplinas_repository.InsertAsync(novaDiscplina, cancellationToken);

            if (result == 0)
            {
                _disciplinas_repository.Rollback();
                return Result.Fail("Não foi possível cadastrar a disciplina.");
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
