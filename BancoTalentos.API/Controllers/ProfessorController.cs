using BancoTalentos.Domain.Services.Pessoas.Base.Dto;
using BancoTalentos.Domain.Services.Pessoas.Professores.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SenacPlataform.Shared.Controllers;

namespace BancoTalentos.API.Controllers;

[ApiController]
[Route("professores")]
public class ProfessorController(ICadastrarProfessorService cadastrarProfessorService,
                        IConsultaProfessorService consultaProfessorService,
                        IAtualizarProfessorService atualizarProfessorService,
                        IDeletarProfessorService deletarProfessorService) : ControllerBase
{
    [Add]
    public async Task<IActionResult> CadastrarAsync(ProfessorDto dto, CancellationToken cancellationToken = default)
    {
        var result = await cadastrarProfessorService.CadastrarAsync(dto, cancellationToken);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [GetAll]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var result = await consultaProfessorService.GetAllAsync(cancellationToken);
        return Ok(result);
    }

    [GetById]
    public async Task<IActionResult> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var result = await consultaProfessorService.GetByIdAsync(id, cancellationToken);
        return Ok(result);
    }

    [Update]
    public async Task<IActionResult> AtualizarAsync(ProfessorDto dto, CancellationToken cancellationToken = default)
    {
        var result = await atualizarProfessorService.AtualizarAsync(dto, cancellationToken);
        return Ok(result);
    }

    [Delete]
    public async Task<IActionResult> DeletarAsync(int id, CancellationToken cancellationToken = default)
    {
        var result = await deletarProfessorService.DeletarProfessorAsync(id, cancellationToken);
        return Ok(result);
    }

    [HttpGet("foto")]
    public async Task<IActionResult> GetFotoPerfil(int id, CancellationToken cancellationToken = default)
    {
        var result = await consultaProfessorService.GetFotoPerfilAsync(id, cancellationToken);
        return result is null ? NotFound() : File(result.Value.ImagemMemory, result.Value.MimeType);
    }
}
