namespace BancoTalentos.Domain.Services.Converter;

public class ByteConverter
{
    private const double BytesPerMB = 1024 * 1024;
    private const double BytesPerGB = 1024 * 1024 * 1024;

    /// <summary>
    /// Converte bytes para megabytes (MB).
    /// </summary>
    /// <param name="bytes">Quantidade de bytes.</param>
    /// <returns>Valor em megabytes.</returns>
    public static double BytesToMB(long bytes)
    {
        return bytes / BytesPerMB;
    }

    /// <summary>
    /// Converte bytes para gigabytes (GB).
    /// </summary>
    /// <param name="bytes">Quantidade de bytes.</param>
    /// <returns>Valor em gigabytes.</returns>
    public static double BytesToGB(long bytes)
    {
        return bytes / BytesPerGB;
    }
}
