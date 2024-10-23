using BancoTalentos.Domain.Services.Imagem.Dto;
using FluentResults;
using Microsoft.AspNetCore.Http;
using SenacPlataform.Shared.Config;
using SenacPlataform.Shared.Converter;
using SenacPlataform.Shared.Enviroment.Interfaces;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Formats.Tiff;
using SixLabors.ImageSharp.Formats.Webp;
using System.Net.Mime;

namespace BancoTalentos.Domain.Services.Imagem;

internal class ImagemService(ImageConfig configuration, IApplicationEnviroment applicationEnviroment) : IImagemService
{
    public async Task<Result<string>> ArmazenarFotoPerfilOnDiskAsync(IFormFile foto, CancellationToken cancellationToken = default)
    {
        string fileName = $"{Guid.NewGuid()}{configuration.Profile.FileNameSuffix ?? "_fotoPerfil"}{Path.GetExtension(foto.FileName)}";
        return await ArmazenarFotoOnDiskAsync(foto, fileName, configuration.Profile.MaxSizeBytes, cancellationToken);
    }

    public async Task<Result<string>> ArmazenarFotoOnDiskAsync(IFormFile foto, string fileName, int maxSizeBytes, CancellationToken cancellationToken = default)
    {
        var resultadoValidacao = ValidarEntrada(foto, maxSizeBytes);

        if (resultadoValidacao.IsFailed)
        {
            return resultadoValidacao;
        }

        var filePath = GetPathOnEnvironment();
        Directory.CreateDirectory(filePath);
        filePath = Path.Combine(filePath, fileName);

        await SaveImageAsync(foto, filePath, 0, cancellationToken);

        return Result.Ok(fileName);
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
        var path = GetPathOnEnvironment();

        path = Path.Combine(path, fileName);

        if (!File.Exists(path))
        {
            throw new FileNotFoundException("O arquivo da imagem não foi encontrado.", path);
        }

        using (var image = await Image.LoadAsync(path, cancellationToken))
        {
            var stream = new MemoryStream();
            await image.SaveAsync(stream, new PngEncoder(), cancellationToken);

            stream.Seek(0, SeekOrigin.Begin);

            return new(image.Metadata.DecodedImageFormat.DefaultMimeType ?? MediaTypeNames.Image.Jpeg, stream);
        }
    }
    private async Task SaveImageAsync(IFormFile foto, string filePath, int compressionAmount, CancellationToken cancellationToken)
    {
        using (var memoryStream = new MemoryStream())
        {
            await foto.CopyToAsync(memoryStream, cancellationToken);
            memoryStream.Position = 0;

            using (var image = Image.Load(memoryStream))
            {
                CompressImage(image, compressionAmount);
                var encoder = GetEncoder(foto.ContentType);

                if (encoder is null) await image.SaveAsync(filePath, cancellationToken);
                else await image.SaveAsync(filePath, encoder, cancellationToken);
            }
        }
    }

    private static void CompressImage(Image image, int compressionAmount)
    {
        if (compressionAmount > 0)
        {
            for (int i = 0; i < compressionAmount; i++)
            {
                //image.Mutate(x => x.Resize(configuration.Width, configuration.Height));
            }
        }
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

    private ImageEncoder? GetEncoder(string contentType)
    {
        return contentType switch
        {
            MediaTypeNames.Image.Jpeg => new JpegEncoder { Quality = configuration.Quality },
            MediaTypeNames.Image.Png => new PngEncoder(),
            MediaTypeNames.Image.Webp => new WebpEncoder(),
            MediaTypeNames.Image.Tiff => new TiffEncoder(),
            _ => null,
        };
    }

    private Result ValidarEntrada(IFormFile foto, int maxSizeBytes)
    {
        if (foto is null || foto.Length == 0)
        {
            return Result.Fail("Nenhuma foto enviada.");
        }

        var validationResult = ImageConfig.Validate(configuration);

        if (!configuration.AllowedFormats.Contains(foto.ContentType))
        {
            validationResult.WithError("Formato de imagem inválido.");
        }

        if (foto.Length > maxSizeBytes)
        {
            validationResult.WithError($"Tamanho da imagem é maior do que o permitido. Tamanho permitido é {ByteConverter.BytesToMB(maxSizeBytes)}Mb");
        }

        return validationResult.IsFailed ? validationResult : Result.Ok();
    }
}