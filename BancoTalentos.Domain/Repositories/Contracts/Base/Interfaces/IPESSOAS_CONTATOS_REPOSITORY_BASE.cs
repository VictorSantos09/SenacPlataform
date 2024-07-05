using BancoTalentos.Domain.Entity;
using BancoTalentos.Domain.Entity.Base;
using BancoTalentos.Domain.Repositories.Contracts.Shared;
using Dapper;

namespace BancoTalentos.Domain.Repositories.Contracts.Base.Interfaces;

public interface IPESSOAS_CONTATOS_REPOSITORY_BASE : IRepository
{
    public Task<IEnumerable<PESSOAS_CONTATOS_BASE>> GetAllAsync(
        CancellationToken cancellationToken = default
    );
    public Task<PESSOAS_CONTATOS?> GetByIdAsync(
        object id,
        CancellationToken cancellationToken = default
    );
    public Task<int> InsertAsync(
        PESSOAS_CONTATOS_BASE entity,
        CancellationToken cancellationToken = default
    );
    public Task<int> UpdateAsync(
        PESSOAS_CONTATOS_BASE entity,
        CancellationToken cancellationToken = default
    );
    public Task<int> DeleteAsync(
        PESSOAS_CONTATOS_BASE entity,
        CancellationToken cancellationToken = default
    );
}
