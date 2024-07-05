using BancoTalentos.Domain.Repositories;
using BancoTalentos.Domain.Services.Professores;
using BancoTalentos.Domain.Services.Professores.Dto;
using BancoTalentos.Domain.Services.Professores.Interfaces;
using BancoTalentos.Domain.Validators;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BancoTalentos.API.Controllers;

[ApiController]
[Route("pessoas")]
public class PessoaController : ControllerBase
{
    private readonly ICadastrarProfessorService _cadastrarProfessorService;
    private readonly IConsultaProfessorService _consultaProfessorService;
    private readonly IAtualizarProfessorService _atualizarProfessorService;

    public PessoaController(IDbConnection conn, IAtualizarProfessorService atualizarProfessorService)
    {
        _cadastrarProfessorService = new CadastrarProfessorService(new PESSOAS_REPOSITORY(conn),
                                                                   new PESSOAS_CONTATOS_REPOSITORY(conn),
                                                                   new TIPOS_CONTATOS_REPOSITORY(conn),
                                                                   new ProfessorValidator(),
                                                                   new PESSOAS_HABILIDADES_DISCIPLINAS_REPOSITORY(conn),
                                                                   new DISCIPLINAS_REPOSITORY(conn));

        _consultaProfessorService = new ConsultaProfessorService(new PESSOAS_REPOSITORY(conn));
        _atualizarProfessorService = atualizarProfessorService;
    }

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
        var result = 
    }
}
