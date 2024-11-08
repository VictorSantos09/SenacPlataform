namespace SenacPlataform.Shared.Controllers;

public readonly struct ExportEndpoint
{
    public static string CSV { get; } = $"{BASE_URL}/csv";
    public static string EXCEL { get; } = $"{BASE_URL}/excel";
    private const string BASE_URL = "http://localhost:5183/api/export";
}
