using FluentResults;
using Microsoft.AspNetCore.Http;

namespace BancoTalentos.Domain.Services.Foto;
public interface IFotoService
{
    Task<Result<string>> ArmazenarFotoOnDiskAsync(IFormFile foto, string fileName, string? filePath = null, CancellationToken cancellationToken = default);
    Task<Result<string>> ArmazenarFotoPerfilOnDiskAsync(IFormFile foto, CancellationToken cancellationToken = default);
}