using BancoTalentos.Domain.Services.Professores.Dto;
using BancoTalentos.Domain.Services.Professores.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BancoTalentos.API.Controllers;

[ApiController]
[Route("pessoas")]
public class PessoaController(ICadastrarProfessorService cadastrarProfessorService,
                        IConsultaProfessorService consultaProfessorService,
                        IAtualizarProfessorService atualizarProfessorService,
                        IDeletarProfessorService deletarProfessorService) : ControllerBase
{
    private readonly ICadastrarProfessorService _cadastrarProfessorService = cadastrarProfessorService;
    private readonly IConsultaProfessorService _consultaProfessorService = consultaProfessorService;
    private readonly IAtualizarProfessorService _atualizarProfessorService = atualizarProfessorService;
    private readonly IDeletarProfessorService _deletarProfessorService = deletarProfessorService;

    [HttpPost]
    public async Task<IActionResult> CadastrarProfessorAsync(ProfessorDto dto, CancellationToken cancellationToken = default)
    {
        var result = await _cadastrarProfessorService.CadastrarAsync(dto, cancellationToken);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
    {
        var result = await _consultaProfessorService.GetAllAsync(cancellationToken);
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> AtualizarAsync(ProfessorDto dto, CancellationToken cancellationToken = default)
    {
        var result = await _atualizarProfessorService.AtualizarAsync(dto,cancellationToken);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> DeletarAsync(int id, CancellationToken cancellationToken = default)
    {
        var result =  await _deletarProfessorService.DeletarAsync(id, cancellationToken);
        return Ok(result);
    }
}
