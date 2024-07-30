using FluentResults;

namespace BancoTalentos.Domain.Services.Pessoas.Coordenador.Interfaces;

public interface IDeletarCoordenadorService
{
    Task<Result> DeletarCoordenadorAsync(int id, CancellationToken cancellationToken = default);
}