using BancoTalentos.Domain.Entity.Base;
using Dapper;

namespace BancoTalentos.Domain.Repositories.Contracts.Base.Interfaces;

public interface ITURMAS_DISCIPLINAS_REPOSITORY_BASE
{
    public Task<IEnumerable<TURMAS_DISCIPLINAS_BASE>> GetAllAsync(
        CancellationToken cancellationToken = default
    );
    public Task<TURMAS_DISCIPLINAS_BASE?> GetByIdAsync(
        object id,
        CancellationToken cancellationToken = default
    );
    public Task<int> InsertAsync(
        TURMAS_DISCIPLINAS_BASE entity,
        CancellationToken cancellationToken = default
    );
    public Task<int> UpdateAsync(
        TURMAS_DISCIPLINAS_BASE entity,
        CancellationToken cancellationToken = default
    );
    public Task<int> DeleteAsync(
        TURMAS_DISCIPLINAS_BASE entity,
        CancellationToken cancellationToken = default
    );
}
