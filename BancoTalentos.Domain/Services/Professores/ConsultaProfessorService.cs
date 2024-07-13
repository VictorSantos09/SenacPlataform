using BancoTalentos.Domain.Entity;
using BancoTalentos.Domain.Repositories.Contracts.Interfaces;
using BancoTalentos.Domain.Services.Professores.Interfaces;
using FluentResults;

namespace BancoTalentos.Domain.Services.Professores;

public class ConsultaProfessorService : IConsultaProfessorService
{
    private readonly IPESSOAS_REPOSITORY _pessoas_repository;

    public ConsultaProfessorService(IPESSOAS_REPOSITORY pessoas_repository)
    {
        _pessoas_repository = pessoas_repository;
    }

    public async Task<Result<IEnumerable<PESSOAS>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var result = await _pessoas_repository.GetAllAsync(cancellationToken);

        return result.Any()
            ? Result.Ok(result)
            : Result.Fail(ProfessorMessages.NenhumProfessorEncontrado);
    }

    public async Task<Result<PESSOAS>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var result = await _pessoas_repository.GetByIdAsync(id, cancellationToken);

        return result is not null
            ? Result.Ok(result)
            : Result.Fail(ProfessorMessages.ProfessorNaoEncontrado);
    }
}
