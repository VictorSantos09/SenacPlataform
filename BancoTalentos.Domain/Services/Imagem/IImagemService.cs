using BancoTalentos.Domain.Services.Imagem.Dto;
using FluentResults;
using Microsoft.AspNetCore.Http;

namespace BancoTalentos.Domain.Services.Imagem;
public interface IImagemService
{
    Task<Result<string>> ArmazenarFotoOnDiskAsync(ImagemBase64DTO dto, int maxSizeBytes, CancellationToken cancellationToken = default);
    Task<Result<string>> ArmazenarFotoPerfilOnDiskAsync(ImagemBase64DTO dto, CancellationToken cancellationToken = default);
    void DeletarImagemOnDisk(string fileName);
    Task<ImagemDTO> GetImagemOnDisk(string fileName, CancellationToken cancellationToken = default);
}