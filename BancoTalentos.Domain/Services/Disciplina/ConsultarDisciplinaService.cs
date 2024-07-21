using BancoTalentos.Domain.Repositories.Contracts.Interfaces;
using BancoTalentos.Domain.Services.Disciplina.Dto;
using BancoTalentos.Domain.Services.Disciplina.Interfaces;
using FluentResults;

namespace BancoTalentos.Domain.Services.Disciplina;
internal class ConsultarDisciplinaService : IConsultarDisciplinaService
{
    private readonly IDISCIPLINAS_REPOSITORY _disciplinas_repository;
    private const string MESSAGE_NAO_ENCONTRADO = "Disciplina não encontrada.";

    public ConsultarDisciplinaService(IDISCIPLINAS_REPOSITORY disciplinas_repository)
    {
        _disciplinas_repository = disciplinas_repository;
    }

    public async Task<Result<IEnumerable<DisciplinaDto>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var result = await _disciplinas_repository.GetAllAsync(cancellationToken);

        if (result is null)
        {
            return Result.Fail("Não foi possível consultar as disciplinas.");
        }

        var disciplinas = result.Select(DisciplinaDto.Create);

        return Result.Ok(disciplinas);
    }

    public async Task<Result<DisciplinaDto>> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var result = await _disciplinas_repository.GetByIdAsync(id, cancellationToken);

        if (result is null)
        {
            return Result.Fail(MESSAGE_NAO_ENCONTRADO);
        }

        return Result.Ok(DisciplinaDto.Create(result));
    }

    public async Task<Result<DisciplinaDto>> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        var result = await _disciplinas_repository.GetByNameAsync(name, cancellationToken);

        if (result is null)
        {
            return Result.Fail(MESSAGE_NAO_ENCONTRADO);
        }

        return Result.Ok(DisciplinaDto.Create(result));
    }
}
