using BancoTalentos.Domain.Services.Pessoas.Professores.Dto;
using FluentResults;

namespace BancoTalentos.Domain.Services.Pessoas.Professores.Interfaces;

public interface ICadastrarProfessorService
{
    Task<Result> CadastrarAsync(ProfessorDto dto, CancellationToken cancellationToken);
}
