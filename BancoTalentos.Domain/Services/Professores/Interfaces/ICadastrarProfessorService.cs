using BancoTalentos.Domain.Services.Professores.Dto;
using FluentResults;

namespace BancoTalentos.Domain.Services.Professores.Interfaces;

public interface ICadastrarProfessorService
{
    Task<Result> CadastrarAsync(ProfessorDto dto, CancellationToken cancellationToken);
}
