using BancoTalentos.Domain.Entity;
using FluentResults;
using SixLabors.ImageSharp;

namespace BancoTalentos.Domain.Services.Pessoas.Coordenador.Interfaces;

public interface IConsultaCoordenadorService
{
    Task<Result<MemoryStream>> GetFotoPerfilAsync(int id, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<PESSOAS>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Result<PESSOAS>> GetByIdAsync(int id, CancellationToken cancellationToken = default);
}