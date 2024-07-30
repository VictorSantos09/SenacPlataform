using FluentResults;

namespace BancoTalentos.Domain.Services.Pessoas.Professores.Interfaces;
public interface IDeletarProfessorService
{
    Task<Result> DeletarProfessorAsync(int id, CancellationToken cancellationToken = default);
}