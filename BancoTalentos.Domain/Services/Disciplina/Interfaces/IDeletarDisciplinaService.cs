using FluentResults;

namespace BancoTalentos.Domain.Services.Disciplina.Interfaces;
public interface IDeletarDisciplinaService
{
    Task<Result> DeletarAsync(int id, CancellationToken cancellationToken);
}