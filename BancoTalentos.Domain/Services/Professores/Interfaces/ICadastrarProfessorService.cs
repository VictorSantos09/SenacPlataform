using BancoTalentos.Domain.Services.Professores.Dto;
using LanguageExt.Common;

namespace BancoTalentos.Domain.Services.Professores.Interfaces;

public interface ICadastrarProfessorService
{
    Task<Result<bool>> CadastrarAsync(ProfessorDto dto, CancellationToken cancellationToken);
}
