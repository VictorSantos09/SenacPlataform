using BancoTalentos.Domain.Entity;
using FluentResults;

namespace BancoTalentos.Domain.Services.Pessoas.Professores.Interfaces;

public interface IConsultaProfessorService
{
    Task<Result<IEnumerable<PESSOAS>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Result<PESSOAS>> GetByIdAsync(int id, CancellationToken cancellationToken = default);
}