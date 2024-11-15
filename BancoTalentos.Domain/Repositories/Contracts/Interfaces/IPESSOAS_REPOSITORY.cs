using BancoTalentos.Domain.Entity;
using BancoTalentos.Domain.Entity.Enums;
using BancoTalentos.Domain.Repositories.Contracts.Base.Interfaces;
using BancoTalentos.Domain.Repositories.Dto;

namespace BancoTalentos.Domain.Repositories.Contracts.Interfaces;

public interface IPESSOAS_REPOSITORY : IPESSOAS_REPOSITORY_BASE
{
    Task<IEnumerable<PESSOAS>> GetAllByCargoAsync(CARGO cargo, CancellationToken cancellationToken = default);
    Task<IEnumerable<ContatoInfo>> BuscaContatosInfo(int idPessoa);
    Task<IEnumerable<HabilidadeInfo>> BuscaHabilidades(int idPessoa);
}
