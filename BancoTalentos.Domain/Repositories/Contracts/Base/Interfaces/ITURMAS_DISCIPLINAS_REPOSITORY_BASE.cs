using BancoTalentos.Domain.Entity;

namespace BancoTalentos.Domain.Repositories.Contracts.Base.Interfaces;

public interface ITURMAS_DISCIPLINAS_REPOSITORY_BASE
{
    public Task<IEnumerable<TURMAS_DISCIPLINAS>> GetAllAsync(
        CancellationToken cancellationToken = default
    );
    public Task<TURMAS_DISCIPLINAS?> GetByIdAsync(
        object id,
        CancellationToken cancellationToken = default
    );
    public Task<int> InsertAsync(
        TURMAS_DISCIPLINAS entity,
        CancellationToken cancellationToken = default
    );
    public Task<int> UpdateAsync(
        TURMAS_DISCIPLINAS entity,
        CancellationToken cancellationToken = default
    );
    public Task<int> DeleteAsync(
        TURMAS_DISCIPLINAS entity,
        CancellationToken cancellationToken = default
    );
}
