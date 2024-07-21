using BancoTalentos.Domain.Services.Disciplina.Dto;
using BancoTalentos.Domain.Services.Disciplina.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SenacPlataform.Shared.Controllers;

namespace BancoTalentos.API.Controllers;

[ApiController]
[Route("disciplinas")]
public class DisciplinaController : ControllerBase
{
    private readonly ICadastrarDisciplinaService _cadastrarDisciplinaService;
    private readonly IConsultarDisciplinaService _consultarDisciplinaService;
    private readonly IAtualizarDisciplinaService _atualizarDisciplinaService;
    private readonly IDeletarDisciplinaService _deletarDisciplinaService;

    public DisciplinaController(ICadastrarDisciplinaService cadastrarDisciplinaService,
                                IConsultarDisciplinaService consultarDisciplinaService,
                                IAtualizarDisciplinaService atualizarDisciplinaService,
                                IDeletarDisciplinaService deletarDisciplinaService)
    {
        _cadastrarDisciplinaService = cadastrarDisciplinaService;
        _consultarDisciplinaService = consultarDisciplinaService;
        _atualizarDisciplinaService = atualizarDisciplinaService;
        _deletarDisciplinaService = deletarDisciplinaService;
    }

    [Add]
    public async Task<IActionResult> AddAsync(DisciplinaDto dto, CancellationToken cancellationToken)
    {
        var result = await _cadastrarDisciplinaService.CadastrarAsync(dto, cancellationToken);

        return result.IsSuccess ? Ok() : BadRequest(result.Errors);
    }

    [Update]
    public async Task<IActionResult> UpdateAsync(DisciplinaDto dto, CancellationToken cancellationToken)
    {
        var result = await _atualizarDisciplinaService.AtualizarAsync(dto, cancellationToken);

        return result.IsSuccess ? Ok() : BadRequest(result.Errors);
    }

    [Delete]
    public async Task<IActionResult> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var result = await _deletarDisciplinaService.DeletarAsync(id, cancellationToken);

        return result.IsSuccess ? Ok() : BadRequest(result.Errors);
    }

    [GetAll]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        var result = await _consultarDisciplinaService.GetAllAsync(cancellationToken);

        return result.IsSuccess ? Ok(result) : BadRequest(result.Errors);
    }

    [GetById]
    public async Task<IActionResult> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var result = await _consultarDisciplinaService.GetByIdAsync(id, cancellationToken);

        return result.IsSuccess ? Ok(result) : BadRequest(result.Errors);
    }
}
