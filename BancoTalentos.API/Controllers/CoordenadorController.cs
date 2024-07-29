using BancoTalentos.Domain.Services.Pessoas.Coordenador;
using BancoTalentos.Domain.Services.Pessoas.Coordenador.Interfaces;
using BancoTalentos.Domain.Services.Pessoas.Professores.Dto;
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

    [GetById]
    public async Task<IActionResult> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var result = await consultaCoordenadorService.GetByIdAsync(id, cancellationToken);
        return Ok(result);
    }

    [Update]
    public async Task<IActionResult> AtualizarAsync(CoordenadorDto dto, CancellationToken cancellationToken = default)
    {
        var result = await atualizarCoordenadorService.AtualizarAsync(dto, cancellationToken);
        return Ok(result);
    }

    [Delete]
    public async Task<IActionResult> DeletarAsync(int id, CancellationToken cancellationToken = default)
    {
        var result = await deletarCoordenadorService.DeletarCoordenadorAsync(id, cancellationToken);
        return Ok(result);
    }
}
