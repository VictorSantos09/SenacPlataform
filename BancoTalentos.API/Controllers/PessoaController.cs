using BancoTalentos.Domain.Services.Foto;
using Microsoft.AspNetCore.Mvc;

namespace BancoTalentos.API.Controllers;

[ApiController]
[Route("pessoas")]
public class PessoaController : ControllerBase
{
    private readonly IImagemService _imagemService;

    public PessoaController(IImagemService imagemService)
    {
        _imagemService = imagemService;
    }

    [HttpGet("foto")]
    public async Task<IActionResult> GetFotoPerfil(string nomeArquivo, CancellationToken cancellationToken = default)
    {
        var result = await _imagemService.GetImagemOnDisk(nomeArquivo, cancellationToken);

        return result is null ? NotFound() : File(result.ImagemMemory, result.MimeType);
    }
}
