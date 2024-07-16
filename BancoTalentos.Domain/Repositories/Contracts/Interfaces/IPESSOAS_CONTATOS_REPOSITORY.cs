using BancoTalentos.Domain.Entity;
using BancoTalentos.Domain.Repositories.Contracts.Base.Interfaces;
using Dapper;

namespace BancoTalentos.Domain.Repositories.Contracts.Interfaces;

public interface IPESSOAS_CONTATOS_REPOSITORY : IPESSOAS_CONTATOS_REPOSITORY_BASE
{
    Task<bool> HasContatoCadadastrado(string contato, int idPessoa, CancellationToken cancellationToken = default);
    Task<IEnumerable<PESSOAS_CONTATOS>> GetByIdPessoaAsync(int idPessoa, CancellationToken cancellationToken = default);
}
