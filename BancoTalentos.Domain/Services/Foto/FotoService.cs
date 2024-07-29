using BancoTalentos.Domain.Config;
using FluentResults;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Formats.Tiff;
using SixLabors.ImageSharp.Formats.Webp;
using SixLabors.ImageSharp.Processing;
using System.Net.Mime;

namespace BancoTalentos.Domain.Services.Foto;

internal class FotoService(ImageConfig configuration) : IFotoService
{
    public async Task<Result<string>> ArmazenarFotoPerfilOnDiskAsync(IFormFile foto, CancellationToken cancellationToken = default)
    {
        string fileName = $"{Guid.NewGuid()}{configuration.FileNameSuffix ?? "_fotoPerfil"}{Path.GetExtension(foto.FileName)}";
        return await ArmazenarFotoOnDiskAsync(foto, fileName, cancellationToken: cancellationToken);
    }

    public async Task<Result<string>> ArmazenarFotoOnDiskAsync(IFormFile foto, string fileName, string? filePath = null, CancellationToken cancellationToken = default)
    {
        var resultadoValidacao = ValidarEntrada(foto);

        if (resultadoValidacao.IsFailed)
        {
            return resultadoValidacao;
        }

        filePath ??= configuration.Path;

        Directory.CreateDirectory(filePath);

        filePath = Path.Combine(filePath, fileName);

        using (var memoryStream = new MemoryStream())
        {
            await foto.CopyToAsync(memoryStream, cancellationToken);
            memoryStream.Position = 0;

            using (var image = Image.Load(memoryStream))
            {
                image.Mutate(x => x.Resize(configuration.Width, configuration.Height));
                var encoder = GetEncoder(foto.ContentType);

                if (encoder is null) await image.SaveAsync(filePath, cancellationToken);
                else await image.SaveAsync(filePath, encoder, cancellationToken);
            }
        }

        return Result.Ok(fileName);
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

    private Result ValidarEntrada(IFormFile foto)
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

        return validationResult.IsFailed ? validationResult : Result.Ok();
    }
}