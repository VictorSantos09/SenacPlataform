using BancoTalentos.Domain.Entity;
using BancoTalentos.Domain.Repositories.Contracts.Base.Interfaces;
using Dapper;

namespace BancoTalentos.Domain.Repositories.Contracts.Interfaces;

public interface IPESSOAS_HABILIDADES_DISCIPLINAS_REPOSITORY
    : IPESSOAS_HABILIDADES_DISCIPLINAS_REPOSITORY_BASE
{
    Task<bool> HasHabilidadeCadastrada(int idDisciplina, int idPessoa, CancellationToken cancellationToken = default);
    Task<IEnumerable<PESSOAS_HABILIDADES_DISCIPLINAS>> GetByIdPessoaAsync(int idPessoa, CancellationToken cancellationToken = default);
}
