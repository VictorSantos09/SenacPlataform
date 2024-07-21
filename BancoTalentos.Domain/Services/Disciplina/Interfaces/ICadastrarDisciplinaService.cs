using BancoTalentos.Domain.Services.Disciplina.Dto;
using FluentResults;

namespace BancoTalentos.Domain.Services.Disciplina.Interfaces;
public interface ICadastrarDisciplinaService
{
    Task<Result> CadastrarAsync(DisciplinaDto dto, CancellationToken cancellationToken);
}