using BancoTalentos.Domain.Repositories.Contracts.Interfaces;
using BancoTalentos.Domain.Services.Disciplina.Interfaces;
using FluentResults;
using SenacPlataform.Shared.Extensions;

namespace BancoTalentos.Domain.Services.Disciplina;
internal class DeletarDisciplinaService : IDeletarDisciplinaService
{
    private readonly IDISCIPLINAS_REPOSITORY _disciplinas_repository;
    private readonly IPESSOAS_HABILIDADES_DISCIPLINAS_REPOSITORY _pessoas_habilidades_disciplinas_repository;
    //private readonly ICURSOS_DISCIPLINAS_REPOSITORY _cursos_disciplinas_repository;

    public DeletarDisciplinaService(IDISCIPLINAS_REPOSITORY disciplinas_repository,
                                    IPESSOAS_HABILIDADES_DISCIPLINAS_REPOSITORY pessoas_habilidades_disciplinas_repository)
    {
        _disciplinas_repository = disciplinas_repository;
        _pessoas_habilidades_disciplinas_repository = pessoas_habilidades_disciplinas_repository;
    }

    public async Task<Result> DeletarAsync(int id, CancellationToken cancellationToken)
    {
        if (id.IsNotValidIdentifier())
        {
            return Result.Fail("Código da disciplina é inválido.");
        }

        var disciplina = await _disciplinas_repository.GetByIdAsync(id, cancellationToken);

        if (disciplina is null)
        {
            return Result.Fail("Disciplina não encontrada.");
        }

        try
        {
            _disciplinas_repository.BeginTransaction();

            if (await _pessoas_habilidades_disciplinas_repository.ExistsBy_IDX_PESSOAS_HABILIDADES_DISCIPLINAS_002(disciplina.ID, cancellationToken))
            {
                _disciplinas_repository.Rollback();
                return Result.Fail("Existem pessoas com habilidades cadastradas para essa disciplina. Portanto não será permitido deletar.");
            }

            var result = await _disciplinas_repository.DeleteAsync(disciplina, cancellationToken);

            if (result == 0)
            {
                _disciplinas_repository.Rollback();
                return Result.Fail("Não foi possível deletar a disciplina.");
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
