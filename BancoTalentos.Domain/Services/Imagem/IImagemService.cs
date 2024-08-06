using BancoTalentos.Domain.Services.Imagem.Dto;
using FluentResults;
using Microsoft.AspNetCore.Http;

namespace BancoTalentos.Domain.Services.Imagem;
public interface IImagemService
{
    Task<Result<string>> ArmazenarFotoOnDiskAsync(IFormFile foto, string fileName, int maxSizeBytes, CancellationToken cancellationToken = default);
    Task<Result<string>> ArmazenarFotoPerfilOnDiskAsync(IFormFile foto, CancellationToken cancellationToken = default);
    void DeletarImagemOnDisk(string fileName);
    Task<ImagemDTO> GetImagemOnDisk(string fileName, CancellationToken cancellationToken = default);
}