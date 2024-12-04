using BancoTalentos.Domain.Entity;

namespace BancoTalentos.Domain.Repositories.Base.Interfaces;

public interface ITIPOS_CONTATOS_REPOSITORY_BASE
{
    public Task<IEnumerable<TIPOS_CONTATOS>> GetAllAsync(
        CancellationToken cancellationToken = default
    );
    public Task<TIPOS_CONTATOS?> GetByIdAsync(
        int id,
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
