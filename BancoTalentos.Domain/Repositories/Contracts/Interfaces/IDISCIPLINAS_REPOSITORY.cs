using BancoTalentos.Domain.Entity;
using BancoTalentos.Domain.Repositories.Contracts.Base.Interfaces;
using Dapper;

namespace BancoTalentos.Domain.Repositories.Contracts.Interfaces;

public interface IDISCIPLINAS_REPOSITORY : IDISCIPLINAS_REPOSITORY_BASE
{
    Task<bool> ExistsBy_IDX_DISCIPLINAS_001_Async(string nome, CancellationToken cancellationToken);
    Task<DISCIPLINAS?> GetByNameAsync(string nome, CancellationToken cancellationToken);
}
