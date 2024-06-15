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

    public PessoaController(IDbConnection conn)
    {
        _cadastrarProfessorService = new CadastrarProfessorService(new PESSOAS_REPOSITORY(conn),
                                                                   new PESSOAS_CONTATOS_REPOSITORY(conn),
                                                                   new TIPOS_CONTATOS_REPOSITORY(conn),
                                                                   new ProfessorValidator(),
                                                                   new PESSOAS_HABILIDADES_DISCIPLINAS_REPOSITORY(conn),
                                                                   new DISCIPLINAS_REPOSITORY(conn));
    }

    [HttpPost]
    public async Task<IActionResult> CadastrarProfessorAsync(ProfessorDto dto, CancellationToken cancellationToken = default)
    {
        var result = await _cadastrarProfessorService.CadastrarAsync(dto, cancellationToken);
        return Ok();
    }
}
