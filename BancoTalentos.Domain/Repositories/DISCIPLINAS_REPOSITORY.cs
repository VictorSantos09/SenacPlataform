using BancoTalentos.Domain.Entity;
using BancoTalentos.Domain.Repositories.Base;
using BancoTalentos.Domain.Repositories.Contracts.Interfaces;
using Dapper;
using Radzen;
using System.Data;
using System.Threading;

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

    public async Task<IEnumerable<DISCIPLINAS>> GetAll_DetalhadoAsync()
    {
        var sql = @"SELECT d.*,
                   COUNT(phd.ID_DISCIPLINA) AS QTD_PESSOAS_CAPACITADAS
            FROM disciplinas d
            LEFT JOIN pessoas_habilidades_disciplinas phd ON phd.ID_DISCIPLINA = d.ID
            GROUP BY d.ID"
        ;

        CommandDefinition command = new(sql);
        return await _connection.QueryAsync<DISCIPLINAS>(command);
    }
}
