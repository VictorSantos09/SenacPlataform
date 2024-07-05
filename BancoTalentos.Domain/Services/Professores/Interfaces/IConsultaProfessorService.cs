using BancoTalentos.Domain.Entity;

namespace BancoTalentos.Domain.Services.Professores.Interfaces;

public interface IConsultaProfessorService
{
    Task<IEnumerable<PESSOAS>> GetAllAsync(CancellationToken cancellationToken = default);
}