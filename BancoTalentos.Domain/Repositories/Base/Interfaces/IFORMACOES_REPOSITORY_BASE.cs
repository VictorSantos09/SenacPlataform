using BancoTalentos.Domain.Entities;

namespace BancoTalentos.Domain.Repositories.Base.Interfaces;

public interface IFORMACOES_REPOSITORY_BASE
{
    public Task<IEnumerable<FORMACOES>> GetAllAsync(
        CancellationToken cancellationToken = default
    );
    public Task<FORMACOES?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default
    );
    public Task<int> InsertAsync(
        FORMACOES entity,
        CancellationToken cancellationToken = default
    );
    public Task<int> UpdateAsync(
        FORMACOES entity,
        CancellationToken cancellationToken = default
    );
    public Task<int> DeleteAsync(
        FORMACOES entity,
        CancellationToken cancellationToken = default
    );
}
