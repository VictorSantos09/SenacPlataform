using BancoTalentos.Domain.Entity;
using BancoTalentos.Domain.Entity.Enums;
using BancoTalentos.Domain.Repositories.Base;
using BancoTalentos.Domain.Repositories.Contracts.Interfaces;
using Dapper;
using System.Data;

namespace BancoTalentos.Domain.Repositories;

public class PESSOAS_REPOSITORY : PESSOAS_REPOSITORY_BASE, IPESSOAS_REPOSITORY
{
    public PESSOAS_REPOSITORY(IDbConnection conn) : base(conn)
    {
    }

    public async Task<IEnumerable<PESSOAS>> GetAllByCargoAsync(CARGO cargo, CancellationToken cancellationToken = default)
    {
        var sql = $"SELECT * FROM PESSOAS WHERE CARGO = '{cargo}'";

        CommandDefinition command = new(sql, cancellationToken: cancellationToken);
        return await _connection.QueryAsync<PESSOAS>(command);
    }
}
