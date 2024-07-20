using BancoTalentos.Domain.Services.TipoContato.Dto;
using BancoTalentos.Domain.Services.TipoContato.Interface;
using Microsoft.AspNetCore.Mvc;
using SenacPlataform.Shared.Controllers;

namespace BancoTalentos.API.Controllers;

[ApiController]
[Route("tipos-contatos")]
public class TipoContatoController(ITipoContatoAdicionarService tipoContatoAdicionarService,
                                   ITipoContatoAtualizarService tipoContatoAtualizarService,
                                   ITipoContatoConsultaService tipoContatoConsultaService,
                                   ITipoContatoInativacaoService tipoContatoInativacaoService,
                                   ITipoContatoDeletarService tipoContatoDeletarService) : ControllerBase
{
    [Add]
    public async Task<IActionResult> AdicionarAsync(TipoContatoDto dto, CancellationToken cancellationToken)
    {
        var result = await tipoContatoAdicionarService.AdicionarAsync(dto, cancellationToken);
        return result.IsSuccess ? Ok() : BadRequest(result.Errors);
    }

    [Update]
    public async Task<IActionResult> AtualizarAsync(TipoContatoDto dto, CancellationToken cancellationToken)
    {
        var result = await tipoContatoAtualizarService.AtualizarAsync(dto, cancellationToken);
        return result.IsSuccess ? Ok() : BadRequest(result.Errors);
    }

    [GetAll]
    public async Task<IActionResult> ConsultarAsync(CancellationToken cancellationToken)
    {
        var result = await tipoContatoConsultaService.GetAllAsync(cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : NoContent();
    }

    [GetById]
    public async Task<IActionResult> ConsultarPorIdAsync(int id, CancellationToken cancellationToken)
    {
        var result = await tipoContatoConsultaService.GetByIdAsync(id, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : NoContent();
    }

    [Delete]
    public async Task<IActionResult> DeletarAsync(int id, CancellationToken cancellationToken)
    {
        var result = await tipoContatoDeletarService.DeletarAsync(id, cancellationToken);
        return result.IsSuccess ? Ok() : BadRequest(result.Errors);
    }

    [HttpPatch("{id}/ativacao")]
    public async Task<IActionResult> AlterarAtivacaoAsync(int id, bool ativar, CancellationToken cancellationToken)
    {
        var result = await tipoContatoInativacaoService.AlterarAtivacaoAsync(id, ativar, cancellationToken);
        return result.IsSuccess ? Ok() : BadRequest(result.Errors);
    }
}
