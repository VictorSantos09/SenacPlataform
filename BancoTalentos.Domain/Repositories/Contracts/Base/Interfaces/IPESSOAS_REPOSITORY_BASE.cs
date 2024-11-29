using BancoTalentos.Domain.Entity;
using BancoTalentos.Domain.Repositories.Contracts.Shared;

namespace BancoTalentos.Domain.Repositories.Contracts.Base.Interfaces;

public interface IPESSOAS_REPOSITORY_BASE : IRepository
{
    public Task<IEnumerable<PESSOAS>> GetAllAsync(
        CancellationToken cancellationToken = default
    );
    public Task<PESSOAS?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default
    );
    public Task<int> InsertAsync(
        PESSOAS entity,
        CancellationToken cancellationToken = default
    );
    public Task<int> UpdateAsync(
        PESSOAS entity,
        CancellationToken cancellationToken = default
    );
    public Task<int> DeleteAsync(
        PESSOAS entity,
        CancellationToken cancellationToken = default
    );
    Task<int> GetMaxIdAsync();
}
