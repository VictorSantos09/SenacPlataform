using BancoTalentos.Domain.Entity;
using BancoTalentos.Domain.Repositories.Contracts.Shared;

namespace BancoTalentos.Domain.Repositories.Contracts.Base.Interfaces;

public interface ITIPOS_CONTATOS_REPOSITORY_BASE : IRepository
{
    public Task<IEnumerable<TIPOS_CONTATOS>> GetAllAsync(
        CancellationToken cancellationToken = default
    );
    public Task<TIPOS_CONTATOS?> GetByIdAsync(
        object id,
        CancellationToken cancellationToken = default
    );
    public Task<int> InsertAsync(
        TIPOS_CONTATOS entity,
        CancellationToken cancellationToken = default
    );
    public Task<int> UpdateAsync(
        TIPOS_CONTATOS entity,
        CancellationToken cancellationToken = default
    );
    public Task<int> DeleteAsync(
        TIPOS_CONTATOS entity,
        CancellationToken cancellationToken = default
    );
}
