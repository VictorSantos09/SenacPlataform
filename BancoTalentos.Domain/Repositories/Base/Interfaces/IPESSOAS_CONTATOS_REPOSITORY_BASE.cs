using BancoTalentos.Domain.Entity;

namespace BancoTalentos.Domain.Repositories.Base.Interfaces;

public interface IPESSOAS_CONTATOS_REPOSITORY_BASE
{
    public Task<IEnumerable<PESSOAS_CONTATOS>> GetAllAsync(
        CancellationToken cancellationToken = default
    );
    public Task<PESSOAS_CONTATOS?> GetByIdAsync(
        int id,
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
