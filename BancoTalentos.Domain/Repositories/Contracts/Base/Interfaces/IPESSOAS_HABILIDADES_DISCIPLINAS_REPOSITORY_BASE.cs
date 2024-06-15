using BancoTalentos.Domain.Entity.Base;
using Dapper;

namespace BancoTalentos.Domain.Repositories.Contracts.Base.Interfaces;

public interface IPESSOAS_HABILIDADES_DISCIPLINAS_REPOSITORY_BASE
{
    public Task<IEnumerable<PESSOAS_HABILIDADES_DISCIPLINAS_BASE>> GetAllAsync(
        CancellationToken cancellationToken = default
    );
    public Task<PESSOAS_HABILIDADES_DISCIPLINAS_BASE?> GetByIdAsync(
        object id,
        CancellationToken cancellationToken = default
    );
    public Task<int> InsertAsync(
        PESSOAS_HABILIDADES_DISCIPLINAS_BASE entity,
        CancellationToken cancellationToken = default
    );
    public Task<int> UpdateAsync(
        PESSOAS_HABILIDADES_DISCIPLINAS_BASE entity,
        CancellationToken cancellationToken = default
    );
    public Task<int> DeleteAsync(
        PESSOAS_HABILIDADES_DISCIPLINAS_BASE entity,
        CancellationToken cancellationToken = default
    );
}
