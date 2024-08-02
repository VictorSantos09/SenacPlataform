using BancoTalentos.Domain.Services.Pessoas.Base.Dto;
using BancoTalentos.Domain.Services.Pessoas.Coordenador.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SenacPlataform.Shared.Controllers;

namespace BancoTalentos.API.Controllers;

[ApiController]
[Route("coordenadores")]
public class CoordenadorController(IConsultaCoordenadorService consultaCoordenadorService,
                                   ICadastrarCoordenadorService cadastrarCoordenadorService,
                                   IDeletarCoordenadorService deletarCoordenadorService,
                                   IAtualizarCoordenadorService atualizarCoordenadorService) : ControllerBase
{
    [Add]
    public async Task<IActionResult> CadastrarAsync(CoordenadorDto dto, CancellationToken cancellationToken = default)
    {
        var result = await cadastrarCoordenadorService.CadastrarAsync(dto, cancellationToken);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [GetAll]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var result = await consultaCoordenadorService.GetAllAsync(cancellationToken);
        return Ok(result);
    }

    [HttpGet("foto")]
    public async Task<IActionResult> GetImagem(int id, CancellationToken cancellationToken = default)
    {
        var result = await consultaCoordenadorService.GetFotoPerfilAsync(id, cancellationToken);
        return File(result.Value, "image/png");
    }

    [GetById]
    public async Task<IActionResult> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var result = await consultaCoordenadorService.GetByIdAsync(id, cancellationToken);
        return Ok(result);
    }

    [Update]
    public async Task<IActionResult> AtualizarAsync(CoordenadorDto dto, CancellationToken cancellationToken = default)
    {
        var result = await atualizarCoordenadorService.AtualizarCoordenadorAsync(dto, cancellationToken);
        return Ok(result);
    }

    [Delete]
    public async Task<IActionResult> DeletarAsync(int id, CancellationToken cancellationToken = default)
    {
        var result = await deletarCoordenadorService.DeletarCoordenadorAsync(id, cancellationToken);
        return Ok(result);
    }
}
