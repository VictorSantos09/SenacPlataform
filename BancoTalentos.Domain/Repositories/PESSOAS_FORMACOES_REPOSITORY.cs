using BancoTalentos.Domain.Repositories.Base;
using BancoTalentos.Domain.Repositories.Contracts.Interfaces;
using System.Data;
using System.Threading;

namespace BancoTalentos.Domain.Repositories;

public class PESSOAS_FORMACOES_REPOSITORY
    : PESSOAS_FORMACOES_REPOSITORY_BASE,
        IPESSOAS_FORMACOES_REPOSITORY
{
    public PESSOAS_FORMACOES_REPOSITORY(IDbConnection _connectionection) : base(_connectionection)
    {
        
    }


    public async Task<bool> TemFormacaoCadastrada(int idPessoa, int idFormacao)
    {
        var sql = @"PESSOAS_FORMACOES
                    WHERE ID_PESSOA = @idPessoa
                    AND ID_FORMACAo = @idFormacao";

        return await IfAsync(sql, new
        {
            idPessoa,
            idFormacao
        }, default);
    }
}
