using FluentResults;
using SenacPlataform.Shared.Extensions;
using System.Net.Mime;

namespace BancoTalentos.Domain.Config;

/// <summary>
/// Contém as configurações gerais para tratamento de imagens. As configurações são definidas no arquivo appsettings.json.
/// <para/>
/// Utiliza a biblioteca ImageSharp para o tratamento das imagens. Documentação: <see href="https://docs.sixlabors.com/articles/imagesharp/index.html?tabs=tabid-1"/> 
/// </summary>
public class ImageConfig
{
    /// <summary>
    /// Largura mínima permitida para a imagem. Define a menor largura aceitável para as imagens.
    /// </summary>
    public int MinWidth { get; init; }

    /// <summary>
    /// Altura mínima permitida para a imagem. Define a menor altura aceitável para as imagens.
    /// </summary>
    public int MinHeight { get; init; }

    /// <summary>
    /// Qualidade da imagem. Deve ser um valor entre 0 e 100. O padrão é 75. 
    /// Esta configuração é aplicada apenas a formatos de imagem que suportam ajuste de qualidade.
    /// </summary>
    public int Quality { get; init; } = 75;

    /// <summary>
    /// Tamanho máximo permitido para o arquivo da imagem, em bytes.
    /// </summary>
    public int MaxSizeBytes { get; init; }

    /// <summary>
    /// Formatos de imagem permitidos. Exemplo: "image/jpeg". 
    /// <para/>
    /// Consulte <see cref="MediaTypeNames.Image"/> para detalhes sobre tipos de mídia suportados.
    /// </summary>
    public required IEnumerable<string> AllowedFormats { get; init; }

    /// <summary>
    /// Configurações específicas para a imagem de perfil.
    /// </summary>
    public required ProfileImageConfig Profile { get; init; }

    /// <summary>
    /// Configurações para diferentes ambientes (desenvolvimento e produção).
    /// </summary>
    public required EnviromentImageConfig Enviroment { get; init; }

    /// <summary>
    /// Valida as configurações da imagem.
    /// </summary>
    /// <param name="imageConfig">O objeto de configuração de imagem a ser validado.</param>
    /// <returns>Um objeto <see cref="Result"/> que indica o sucesso ou falha da validação.</returns>
    public static Result Validate(ImageConfig imageConfig)
    {
        var errors = new List<Error>();

        if (!imageConfig.AllowedFormats.Any())
        {
            errors.Add(new($"Nenhum formato permitido informado. Campo: {nameof(imageConfig.AllowedFormats)}"));
        }

        if (imageConfig.MinWidth <= 0)
        {
            errors.Add(new($"Largura mínima inválida. Campo: {nameof(imageConfig.MinWidth)}"));
        }

        if (imageConfig.MinHeight <= 0)
        {
            errors.Add(new($"Altura mínima inválida. Campo: {nameof(imageConfig.MinHeight)}"));
        }

        if (imageConfig.Quality < 0 || imageConfig.Quality > 100)
        {
            errors.Add(new($"Qualidade inválida. Deve ser um valor entre 0 e 100. Campo: {nameof(imageConfig.Quality)}"));
        }

        if (imageConfig.MaxSizeBytes <= 0)
        {
            errors.Add(new($"Tamanho máximo do arquivo não informado. Campo: {nameof(imageConfig.MaxSizeBytes)}"));
        }

        var resultProfile = ProfileImageConfig.Validate(imageConfig.Profile);
        //var resultEnviroment = EnviromentImageConfig.Validate(imageConfig.Enviroment);

        //errors.AddRange(resultProfile.)

        return errors.Count > 0 ? Result.Fail(errors) : Result.Ok();
    }
}

/// <summary>
/// Contém as configurações específicas para imagens de perfil.
/// </summary>
public class ProfileImageConfig
{
    /// <summary>
    /// Largura mínima permitida para a imagem de perfil.
    /// </summary>
    public int MinWidth { get; init; }

    /// <summary>
    /// Altura mínima permitida para a imagem de perfil.
    /// </summary>
    public int MinHeight { get; init; }

    /// <summary>
    /// Qualidade da imagem de perfil. Deve ser um valor entre 0 e 100. O padrão é 75.
    /// </summary>
    public int Quality { get; init; } = 75;

    /// <summary>
    /// Tamanho máximo permitido para a imagem de perfil, em bytes.
    /// </summary>
    public int MaxSizeBytes { get; init; }

    /// <summary>
    /// Formatos de imagem permitidos para a imagem de perfil. Exemplo: "image/jpeg".
    /// </summary>
    public required IEnumerable<string> AllowedFormats { get; init; }

    /// <summary>
    /// Nome da pasta onde as fotos de perfil serão armazenadas.
    /// </summary>
    public required string FolderName { get; init; }

    /// <summary>
    /// Sufixo do nome do arquivo das imagens de perfil. Exemplo: "_profilePhotoSenacPlataform".
    /// </summary>
    public required string FileNameSuffix { get; init; }

    public static Result Validate(ProfileImageConfig config)
    {
        return Result.Ok();
    }
}

/// <summary>
/// Contém configurações específicas para diferentes ambientes, como desenvolvimento e produção.
/// </summary>
public class EnviromentImageConfig
{
    /// <summary>
    /// Configurações para o ambiente de desenvolvimento.
    /// </summary>
    public required DevelopmentEnviromentImageConfig Development { get; init; }

    /// <summary>
    /// Configurações para o ambiente de produção.
    /// </summary>
    public required ProductionEnviromentImageConfig Production { get; init; }
}

/// <summary>
/// Contém configurações para o ambiente de desenvolvimento.
/// </summary>
public class DevelopmentEnviromentImageConfig
{
    /// <summary>
    /// Indica se um caminho temporário deve ser utilizado para armazenar arquivos.
    /// </summary>
    /// <remarks>
    /// Quando <c>true</c>, o sistema usará um caminho temporário definido em <see cref="PathTempFolderName"/>.
    /// Quando <c>false</c>, o sistema usará um caminho permanente especificado em <see cref="Path"/>.
    /// </remarks>
    public bool UseTempPath { get; set; }

    /// <summary>
    /// Nome da pasta temporária onde os arquivos serão armazenados se <see cref="UseTempPath"/> for <c>true</c>.
    /// </summary>
    public string PathTempFolderName { get; set; }

    /// <summary>
    /// Caminho permanente onde os arquivos serão armazenados se <see cref="UseTempPath"/> for <c>false</c>.
    /// </summary>
    public string Path { get; set; }

    /// <summary>
    /// Valida a instância fornecida de <see cref="DevelopmentEnviromentImageConfig"/> com base nas configurações especificadas.
    /// </summary>
    /// <param name="config">O objeto de configuração a ser validado.</param>
    /// <returns><c>true</c> se a configuração for válida; caso contrário, <c>false</c>.</returns>
    /// <exception cref="ArgumentNullException">Lançado quando o parâmetro <paramref name="config"/> é nulo.</exception>
    public static Result Validate(DevelopmentEnviromentImageConfig config)
    {
        if (config is null)
        {
            return Result.Fail("A configuração não pode ser nula");
        }

        return config.UseTempPath
            ? ValidateTempPathConfig(config.PathTempFolderName)
            : ValidatePermanentPathConfig(config.Path);
    }

    /// <summary>
    /// Valida a configuração quando um caminho temporário é utilizado.
    /// </summary>
    /// <param name="pathTempFolderName">O nome da pasta temporária.</param>
    /// <returns><c>true</c> se a configuração do caminho temporário for válida; caso contrário, <c>false</c>.</returns>
    private static Result ValidateTempPathConfig(string pathTempFolderName)
    {
        return !pathTempFolderName.IsEmpty() ? Result.Ok() : Result.Fail("A pasta no diretório temporário é vázia");
    }

    /// <summary>
    /// Valida a configuração quando um caminho permanente é utilizado.
    /// </summary>
    /// <param name="path">O caminho permanente.</param>
    /// <returns><c>true</c> se a configuração do caminho permanente for válida; caso contrário, <c>false</c>.</returns>
    private static Result ValidatePermanentPathConfig(string path)
    {
        return !path.IsEmpty() ? Result.Ok() : Result.Fail("O caminho permanente informado é vázio");
    }
}

/// <summary>
/// Contém configurações para o ambiente de produção.
/// </summary>
public class ProductionEnviromentImageConfig
{
    /// <summary>
    /// Caminho onde as imagens serão salvas no ambiente de produção.
    /// </summary>
    public required string Path { get; init; }

    public static Result Validate(ProductionEnviromentImageConfig config)
    {
        return config.Path is not null ? Result.Ok() : Result.Fail("O caminho dos arquivos de produção não foi informado");
    }
}
