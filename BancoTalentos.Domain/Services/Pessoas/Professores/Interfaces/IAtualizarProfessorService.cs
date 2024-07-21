using BancoTalentos.Domain.Services.Pessoas.Professores.Dto;
using FluentResults;

namespace BancoTalentos.Domain.Services.Pessoas.Professores.Interfaces;

public interface IAtualizarProfessorService
{
    Task<Result> AtualizarAsync(ProfessorDto dto, CancellationToken cancellationToken = default);
}