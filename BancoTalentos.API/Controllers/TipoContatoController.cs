using BancoTalentos.Domain.Services.TipoContato.Dto;
using BancoTalentos.Domain.Services.TipoContato.Interface;
using Microsoft.AspNetCore.Mvc;
using SenacPlataform.Shared.Controllers;

namespace BancoTalentos.API.Controllers;

[ApiController]
[Route("tipos-contatos")]
public class TipoContatoController(ITipoContatoAdicionarService tipoContatoAdicionarService) : ControllerBase
{
    [Add]
    public async Task<IActionResult> AdicionarAsync(TipoContatoDto dto, CancellationToken cancellationToken)
    {
        var result = await tipoContatoAdicionarService.AdicionarAsync(dto, cancellationToken);
        return result.IsSuccess ? Ok() : BadRequest(result.Errors);
    }
}
