using BancoTalentos.Domain.Entity;
using BancoTalentos.Domain.Repositories.Contracts.Shared;

namespace BancoTalentos.Domain.Repositories.Contracts.Base.Interfaces;

public interface IDISCIPLINAS_REPOSITORY_BASE : IRepository
{
    public Task<IEnumerable<DISCIPLINAS>> GetAllAsync(
        CancellationToken cancellationToken = default
    );
    public Task<DISCIPLINAS?> GetByIdAsync(
        object id,
        CancellationToken cancellationToken = default
    );
    public Task<int> InsertAsync(
        DISCIPLINAS entity,
        CancellationToken cancellationToken = default
    );
    public Task<int> UpdateAsync(
        DISCIPLINAS entity,
        CancellationToken cancellationToken = default
    );
    public Task<int> DeleteAsync(
        DISCIPLINAS entity,
        CancellationToken cancellationToken = default
    );
}
