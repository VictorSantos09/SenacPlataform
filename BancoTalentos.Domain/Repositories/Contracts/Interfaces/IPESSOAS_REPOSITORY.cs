using BancoTalentos.Domain.Entity;
using BancoTalentos.Domain.Entity.Enums;
using BancoTalentos.Domain.Repositories.Contracts.Base.Interfaces;
using Dapper;

namespace BancoTalentos.Domain.Repositories.Contracts.Interfaces;

public interface IPESSOAS_REPOSITORY : IPESSOAS_REPOSITORY_BASE
{
    Task<IEnumerable<PESSOAS>> GetAllByCargoAsync(CARGO cargo, CancellationToken cancellationToken = default);
}
