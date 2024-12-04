using BancoTalentos.Domain.Entities;

namespace BancoTalentos.Domain.Repositories.Base.Interfaces;

public interface IPESSOAS_FORMACOES_REPOSITORY_BASE
{
    public Task<IEnumerable<PESSOAS_FORMACOES>> GetAllAsync(
        CancellationToken cancellationToken = default
    );
    public Task<PESSOAS_FORMACOES?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default
    );
    public Task<int> InsertAsync(
        PESSOAS_FORMACOES entity,
        CancellationToken cancellationToken = default
    );
    public Task<int> UpdateAsync(
        PESSOAS_FORMACOES entity,
        CancellationToken cancellationToken = default
    );
    public Task<int> DeleteAsync(
        PESSOAS_FORMACOES entity,
        CancellationToken cancellationToken = default
    );
}
