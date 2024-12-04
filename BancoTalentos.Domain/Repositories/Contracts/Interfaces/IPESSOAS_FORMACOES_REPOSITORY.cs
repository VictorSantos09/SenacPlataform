using BancoTalentos.Domain.Repositories.Base.Interfaces;

namespace BancoTalentos.Domain.Repositories.Contracts.Interfaces
{
    public interface IPESSOAS_FORMACOES_REPOSITORY : IPESSOAS_FORMACOES_REPOSITORY_BASE
    {
        Task<bool> TemFormacaoCadastrada(int idPessoa, int idFormacao);
    }
}
