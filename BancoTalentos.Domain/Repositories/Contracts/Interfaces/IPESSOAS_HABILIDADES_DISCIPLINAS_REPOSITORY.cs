using BancoTalentos.Domain.Entity;
using BancoTalentos.Domain.Repositories.Contracts.Base.Interfaces;

namespace BancoTalentos.Domain.Repositories.Contracts.Interfaces;

public interface IPESSOAS_HABILIDADES_DISCIPLINAS_REPOSITORY
    : IPESSOAS_HABILIDADES_DISCIPLINAS_REPOSITORY_BASE
{
    Task<bool> HasHabilidadeCadastrada(int idDisciplina, int idPessoa, CancellationToken cancellationToken = default);
    Task<IEnumerable<PESSOAS_HABILIDADES_DISCIPLINAS>> GetByIdPessoaAsync(int idPessoa, CancellationToken cancellationToken = default);
    Task<PESSOAS_HABILIDADES_DISCIPLINAS?> GetBy_IDX_PESSOAS_HABILIDADES_DISCIPLINAS_002(int idPessoa, int idDisciplina, CancellationToken cancellationToken = default);
    Task<bool> ExistsBy_IDX_PESSOAS_HABILIDADES_DISCIPLINAS_002(int idDisciplina, CancellationToken cancellationToken = default);
    Task DeletarHabilidadesPessoa(int idPessoa);
    Task DeleteBy_IDX_PESSOAS_HABILIDADES_DISCIPLINAS_002(int idPessoa, int idDisciplina);
}
