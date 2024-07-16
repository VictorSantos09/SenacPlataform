using BancoTalentos.Domain.Entity;
using BancoTalentos.Domain.Entity.Base;
using Dapper;

namespace BancoTalentos.Domain.Repositories.Contracts.Base.Interfaces;

public interface IPESSOAS_HABILIDADES_DISCIPLINAS_REPOSITORY_BASE
{
    public Task<IEnumerable<PESSOAS_HABILIDADES_DISCIPLINAS>> GetAllAsync(
        CancellationToken cancellationToken = default
    );
    public Task<PESSOAS_HABILIDADES_DISCIPLINAS?> GetByIdAsync(
        object id,
        CancellationToken cancellationToken = default
    );
    public Task<int> InsertAsync(
        PESSOAS_HABILIDADES_DISCIPLINAS entity,
        CancellationToken cancellationToken = default
    );
    public Task<int> UpdateAsync(
        PESSOAS_HABILIDADES_DISCIPLINAS entity,
        CancellationToken cancellationToken = default
    );
    public Task<int> DeleteAsync(
        PESSOAS_HABILIDADES_DISCIPLINAS entity,
        CancellationToken cancellationToken = default
    );
}
