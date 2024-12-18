using BancoTalentos.Domain.Entity;
using BancoTalentos.Domain.Repositories.Contracts.Base.Interfaces;
using BancoTalentos.Domain.Repositories.Dto;

namespace BancoTalentos.Domain.Repositories.Contracts.Interfaces;

public interface IDISCIPLINAS_REPOSITORY : IDISCIPLINAS_REPOSITORY_BASE
{
    Task<bool> ExistsBy_IDX_DISCIPLINAS_001_Async(string nome, CancellationToken cancellationToken);
    Task<IEnumerable<DISCIPLINAS>> GetAll_DetalhadoAsync();
    Task<DISCIPLINAS?> GetByNameAsync(string nome, CancellationToken cancellationToken);
    Task<IEnumerable<DisciplinaDetalhesDTO>> GetDetalhesPessoasHabilitadas(int id);
}
