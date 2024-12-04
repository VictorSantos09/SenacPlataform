using FluentResults;

namespace BancoTalentos.Domain.Services.Pessoas.Coordenador.Interfaces;

public interface IDeletarCoordenadorService
{
    Task<Result> DeletarAsync(int id, CancellationToken cancellationToken = default);
}