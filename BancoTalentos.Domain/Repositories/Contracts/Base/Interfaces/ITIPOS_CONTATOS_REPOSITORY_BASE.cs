using BancoTalentos.Domain.Entity.Base;
using BancoTalentos.Domain.Repositories.Contracts.Shared;
using Dapper;

namespace BancoTalentos.Domain.Repositories.Contracts.Base.Interfaces;

public interface ITIPOS_CONTATOS_REPOSITORY_BASE : IRepository
{
    public Task<IEnumerable<TIPOS_CONTATOS_BASE>> GetAllAsync(
        CancellationToken cancellationToken = default
    );
    public Task<TIPOS_CONTATOS_BASE?> GetByIdAsync(
        object id,
        CancellationToken cancellationToken = default
    );
    public Task<int> InsertAsync(
        TIPOS_CONTATOS_BASE entity,
        CancellationToken cancellationToken = default
    );
    public Task<int> UpdateAsync(
        TIPOS_CONTATOS_BASE entity,
        CancellationToken cancellationToken = default
    );
    public Task<int> DeleteAsync(
        TIPOS_CONTATOS_BASE entity,
        CancellationToken cancellationToken = default
    );
}
