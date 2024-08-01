using FluentResults;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;

namespace BancoTalentos.Domain.Services.Foto;
public interface IImagemService
{
    Task<Result<string>> ArmazenarFotoOnDiskAsync(IFormFile foto, string fileName, int maxSizeBytes, CancellationToken cancellationToken = default);
    Task<Result<string>> ArmazenarFotoPerfilOnDiskAsync(IFormFile foto, CancellationToken cancellationToken = default);
    void DeletarFotoOnDisk(string fileName, CancellationToken cancellationToken = default);
    Image GetImagemOnDisk(string fileName, CancellationToken cancellationToken = default);
}