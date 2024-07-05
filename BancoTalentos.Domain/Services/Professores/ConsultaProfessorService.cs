using BancoTalentos.Domain.Entity;
using BancoTalentos.Domain.Repositories.Contracts.Interfaces;
using BancoTalentos.Domain.Services.Professores.Interfaces;

namespace BancoTalentos.Domain.Services.Professores;

public class ConsultaProfessorService : IConsultaProfessorService
{
    private readonly IPESSOAS_REPOSITORY _pessoas_repository;

    public ConsultaProfessorService(IPESSOAS_REPOSITORY pessoas_repository)
    {
        _pessoas_repository = pessoas_repository;
    }

    public async Task<IEnumerable<PESSOAS>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _pessoas_repository.GetAllAsync(cancellationToken);
    }
}
