using BancoTalentos.Domain.Entity;
using BancoTalentos.Domain.Repositories.Base;
using BancoTalentos.Domain.Repositories.Contracts.Interfaces;
using Dapper;
using System.Data;

namespace BancoTalentos.Domain.Repositories;

public class DISCIPLINAS_REPOSITORY : DISCIPLINAS_REPOSITORY_BASE, IDISCIPLINAS_REPOSITORY
{
    public DISCIPLINAS_REPOSITORY(IDbConnection _connectionection) : base(_connectionection)
    {
    }

    public async Task<bool> ExistsBy_IDX_DISCIPLINAS_001_Async(string nome, CancellationToken cancellationToken)
    {
        var query = "DISCIPLINAS WHERE NOME = @nome";

        return await IfAsync(query, new { nome }, cancellationToken);
    }

    public async Task<DISCIPLINAS?> GetByNameAsync(string nome, CancellationToken cancellationToken)
    {
        var query = "SELECT * FROM DISCIPLINAS WHERE NOME = @nome";

        CommandDefinition command = new(query, new { nome }, cancellationToken: cancellationToken);
        return await _connection.QuerySingleOrDefaultAsync(command);
    }
}
