using BancoTalentos.Domain.Entity.Base;
using BancoTalentos.Domain.Repositories.Contracts.Shared;
using Dapper;

namespace BancoTalentos.Domain.Repositories.Contracts.Base.Interfaces;

public interface IDISCIPLINAS_REPOSITORY_BASE : IRepository
{
    public Task<IEnumerable<DISCIPLINAS_BASE>> GetAllAsync(
        CancellationToken cancellationToken = default
    );
    public Task<DISCIPLINAS_BASE?> GetByIdAsync(
        object id,
        CancellationToken cancellationToken = default
    );
    public Task<int> InsertAsync(
        DISCIPLINAS_BASE entity,
        CancellationToken cancellationToken = default
    );
    public Task<int> UpdateAsync(
        DISCIPLINAS_BASE entity,
        CancellationToken cancellationToken = default
    );
    public Task<int> DeleteAsync(
        DISCIPLINAS_BASE entity,
        CancellationToken cancellationToken = default
    );
}
