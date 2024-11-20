using BancoTalentos.Domain.Services.Imagem.Dto;
using FluentResults;
using SenacPlataform.Shared.Config;
using SenacPlataform.Shared.Converter;
using SenacPlataform.Shared.Enviroment.Interfaces;
using SenacPlataform.Shared.Exceptions;
using SenacPlataform.Shared.Extensions;
using System.Net.Mime;

namespace BancoTalentos.Domain.Services.Imagem;

internal class ImagemService(ImageConfig configuration, IApplicationEnviroment applicationEnviroment) : IImagemService
{
    public async Task<Result<string>> ArmazenarFotoPerfilOnDiskAsync(ImagemBase64DTO dto, CancellationToken cancellationToken = default)
    {
        dto.FileName = $"{Guid.NewGuid()}{configuration.Profile.FileNameSuffix ?? "_fotoPerfil"}__{dto.FileName}";
        return await ArmazenarFotoOnDiskAsync(dto, configuration.Profile.MaxSizeBytes, cancellationToken);
    }

    public string GetPath(string fileName)
    {
        var path = GetPath();
        path = Path.Combine(path, fileName);
        return path;
    }

    public string GetPath()
    {
        var path = GetPathOnEnvironment();
        return path;
    }

    public async Task<Result<string>> ArmazenarFotoOnDiskAsync(ImagemBase64DTO dto, int maxSizeBytes, CancellationToken cancellationToken = default)
    {
        var resultadoValidacao = ValidarEntrada(dto, maxSizeBytes);

        if (resultadoValidacao.IsFailed)
        {
            return resultadoValidacao;
        }

        var filePath = GetPath();
        Directory.CreateDirectory(filePath);
        filePath = Path.Combine(filePath, dto.FileName);

        await SaveImageAsync(dto, filePath, 0, cancellationToken);

        return Result.Ok(dto.FileName);
    }

    public void DeletarImagemOnDisk(string fileName)
    {
        var path = GetPathOnEnvironment();

        path = Path.Combine(path, fileName);
        File.Delete(path);
    }

    /// <summary>
    /// Obtém uma imagem do disco a partir do nome do arquivo.
    /// </summary>
    /// <param name="fileName">Nome do arquivo da imagem.</param>
    /// <param name="cancellationToken">Token de cancelamento para operações canceláveis.</param>
    /// <returns>Objeto Image representando a imagem.</returns>
    public async Task<ImagemDTO> GetImagemOnDisk(string fileName, CancellationToken cancellationToken = default)
    {
        var path = GetPath(fileName);

        if (!File.Exists(path))
        {
            throw new ImageNotFoundException("O arquivo da imagem não foi encontrado.", path);
        }

        // Lê o arquivo de imagem diretamente como array de bytes
        var imageBytes = await File.ReadAllBytesAsync(path, cancellationToken);

        // Converte os bytes da imagem para Base64
        string base64String = Convert.ToBase64String(imageBytes);

        // Determina o tipo MIME com base na extensão do arquivo
        string mimeType = fileName.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ? "image/png" :
                          fileName.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) || fileName.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase) ? "image/jpeg" :
                          MediaTypeNames.Application.Octet;

        return new ImagemDTO(mimeType, base64String);
    }


    private async Task SaveImageAsync(ImagemBase64DTO dto, string filePath, int compressionAmount, CancellationToken cancellationToken)
    {
        byte[] decodedBytes = Convert.FromBase64String(dto.Image);
        File.WriteAllBytes(filePath, decodedBytes);
    }

    /// <summary>
    /// Determina qual é o diretório que deve ser salvo baseado no ambiente onde está rodando a aplicação.
    /// </summary>
    /// <returns>O caminho onde deve ser salvo.</returns>
    private string GetPathOnEnvironment()
    {
        if (applicationEnviroment.IsDevelopment())
        {
            var devConfig = configuration.Enviroment.Development;
            return devConfig.UseTempPath ? $@"{Path.GetTempPath()}\\{devConfig.PathTempFolderName}" : devConfig.Path;
        }

        return configuration.Enviroment.Production.Path;

    }

    private Result ValidarEntrada(ImagemBase64DTO dto, int maxSizeBytes)
    {
        if (dto.Image.IsEmpty() || dto.Size == 0)
        {
            return Result.Fail("Nenhuma foto enviada.");
        }

        var validationResult = ImageConfig.Validate(configuration);

        if (!configuration.AllowedFormats.Contains(dto.ContentType))
        {
            validationResult.WithError("Formato de imagem inválido.");
        }

        if (dto.Size > maxSizeBytes)
        {
            validationResult.WithError($"Tamanho da imagem é maior do que o permitido. Tamanho permitido é {ByteConverter.BytesToMB(maxSizeBytes)}Mb");
        }

        return validationResult.IsFailed ? validationResult : Result.Ok();
    }
}