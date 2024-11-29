using BancoTalentos.Domain.Entity;
using BancoTalentos.Domain.Repositories.Contracts.Base.Interfaces;

namespace BancoTalentos.Domain.Repositories.Contracts.Interfaces;

public interface ITIPOS_CONTATOS_REPOSITORY : ITIPOS_CONTATOS_REPOSITORY_BASE
{
    Task<bool> ExistsBy_IDX_TIPOS_CONTATOS_002_Async(string tipo, CancellationToken cancellationToken);
    Task<TIPOS_CONTATOS> GetEmailAsync();
    Task<TIPOS_CONTATOS> GetTelefoneAsync();
}
