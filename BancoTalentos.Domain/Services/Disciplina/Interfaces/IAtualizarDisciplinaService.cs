using BancoTalentos.Domain.Services.Disciplina.Dto;
using FluentResults;

namespace BancoTalentos.Domain.Services.Disciplina.Interfaces;
public interface IAtualizarDisciplinaService
{
    Task<Result> AtualizarAsync(DisciplinaDto dto, CancellationToken cancellationToken);
}