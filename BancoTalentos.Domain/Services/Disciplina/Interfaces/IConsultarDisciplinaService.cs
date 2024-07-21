using BancoTalentos.Domain.Services.Disciplina.Dto;
using FluentResults;

namespace BancoTalentos.Domain.Services.Disciplina.Interfaces;
public interface IConsultarDisciplinaService
{
    Task<Result<IEnumerable<DisciplinaDto>>> GetAllAsync(CancellationToken cancellationToken);
    Task<Result<DisciplinaDto>> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<Result<DisciplinaDto>> GetByNameAsync(string name, CancellationToken cancellationToken);
}