using FluentResults;
using SenacPlataform.Shared.Extensions;
using System.Net.Mime;

namespace BancoTalentos.Domain.Config;

/// <summary>
/// Contém as configurações de imagem. Definidas no arquivo appsettings.json.
/// 
/// <para/>
/// É utilizado ImageSharp para o tratamento das imagens. Docs: <see href="https://docs.sixlabors.com/articles/imagesharp/index.html?tabs=tabid-1 "/>
/// </summary>
public record ImageConfig
{
    /// <summary>
    /// Lagura da imagem.
    /// </summary>
    public int Width { get; init; }
    /// <summary>
    /// Altura da imagem.
    /// </summary>
    public int Height { get; init; }
    /// <summary>
    /// Qualidade da imagem. Deve ser um valor entre 0 e 100. Padrão é 75. Funciona apenas para formatos de imagem que suportam qualidade.
    /// </summary>
    public int Quality { get; init; } = 75;
    /// <summary>
    /// Formatos de imagem permitidos. Exemplo: "image/jpeg".
    /// <para/>
    /// Consulte <see cref="MediaTypeNames.Image"/> para detalhes.
    /// </summary>
    public IEnumerable<string> AllowedFormats { get; init; }
    /// <summary>
    /// Caminho onde as imagens serão armazenadas.
    /// </summary>
    public string Path { get; init; }
    public string? FileNameSuffix { get; init; }

    public ImageConfig(int width, int height, IEnumerable<string> allowedFormats, string path, string? fileNameSuffix = null)
    {
        Width = width;
        Height = height;
        AllowedFormats = allowedFormats;
        Path = path;
        FileNameSuffix = fileNameSuffix;
    }

    public static Result Validate(ImageConfig imageConfig)
    {
        List<Error> errors = [];

        if (imageConfig.Path.IsEmpty())
        {
            errors.Add(new($"Caminho não informado. Campo: {nameof(imageConfig.Path)}"));
        }

        if (!imageConfig.AllowedFormats.Any())
        {
            errors.Add(new($"Nenhum formato permitido informado. Campo: {nameof(imageConfig.AllowedFormats)}"));
        }

        if (imageConfig.Width <= 0)
        {
            errors.Add(new($"Largura inválida. Campo: {nameof(imageConfig.Width)}"));
        }

        if (imageConfig.Height <= 0)
        {
            errors.Add(new($"Altura inválida. Campo: {nameof(imageConfig.Height)}"));
        }

        if (imageConfig.Quality < 0 || imageConfig.Quality > 100)
        {
            errors.Add(new($"Qualidade inválida. Deve ser um valor entre 0 e 100. Campo: {nameof(imageConfig.Quality)}"));
        }

        return errors.Count != 0 ? Result.Fail(errors) : Result.Ok();
    }
}
