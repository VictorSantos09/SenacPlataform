using BancoTalentos.Domain.Services.Professores.Dto;
using FluentResults;

namespace BancoTalentos.Domain.Services.Professores.Interfaces;

public interface IAtualizarProfessorService
{
    Task<Result> AtualizarAsync(ProfessorDto dto, CancellationToken cancellationToken = default);
}