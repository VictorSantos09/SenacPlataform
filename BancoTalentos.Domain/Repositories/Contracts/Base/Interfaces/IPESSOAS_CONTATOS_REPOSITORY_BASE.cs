using BancoTalentos.Domain.Entity;
using BancoTalentos.Domain.Repositories.Contracts.Shared;

namespace BancoTalentos.Domain.Repositories.Contracts.Base.Interfaces;

public interface IPESSOAS_CONTATOS_REPOSITORY_BASE : IRepository
{
    public Task<IEnumerable<PESSOAS_CONTATOS>> GetAllAsync(
        CancellationToken cancellationToken = default
    );
    public Task<PESSOAS_CONTATOS?> GetByIdAsync(
        object id,
        CancellationToken cancellationToken = default
    );
    public Task<int> InsertAsync(
        PESSOAS_CONTATOS entity,
        CancellationToken cancellationToken = default
    );
    public Task<int> UpdateAsync(
        PESSOAS_CONTATOS entity,
        CancellationToken cancellationToken = default
    );
    public Task<int> DeleteAsync(
        PESSOAS_CONTATOS entity,
        CancellationToken cancellationToken = default
    );
}
