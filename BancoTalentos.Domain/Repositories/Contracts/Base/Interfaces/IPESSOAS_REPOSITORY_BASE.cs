using BancoTalentos.Domain.Entity;
using BancoTalentos.Domain.Entity.Base;
using BancoTalentos.Domain.Repositories.Contracts.Shared;
using Dapper;

namespace BancoTalentos.Domain.Repositories.Contracts.Base.Interfaces;

public interface IPESSOAS_REPOSITORY_BASE : IRepository
{
    public Task<IEnumerable<PESSOAS>> GetAllAsync(
        CancellationToken cancellationToken = default
    );
    public Task<PESSOAS_BASE?> GetByIdAsync(
        object id,
        CancellationToken cancellationToken = default
    );
    public Task<int> InsertAsync(
        PESSOAS_BASE entity,
        CancellationToken cancellationToken = default
    );
    public Task<int> UpdateAsync(
        PESSOAS_BASE entity,
        CancellationToken cancellationToken = default
    );
    public Task<int> DeleteAsync(
        PESSOAS_BASE entity,
        CancellationToken cancellationToken = default
    );
    Task<int> GetMaxIdAsync();
}
