using BancoTalentos.Domain.Entity;
using BancoTalentos.Domain.Repositories.Base;
using BancoTalentos.Domain.Repositories.Contracts.Interfaces;
using Dapper;
using System.Data;

namespace BancoTalentos.Domain.Repositories;

public class TIPOS_CONTATOS_REPOSITORY
    : TIPOS_CONTATOS_REPOSITORY_BASE,
        ITIPOS_CONTATOS_REPOSITORY
{
    public TIPOS_CONTATOS_REPOSITORY(IDbConnection _connectionection) : base(_connectionection)
    {
    }

    public async Task<bool> ExistsBy_IDX_TIPOS_CONTATOS_002_Async(string tipo, CancellationToken cancellationToken)
    {
        string sql = $"SELECT IF((SELECT COUNT(1) FROM TIPOS_CONTATOS WHERE TIPO = @tipo), true, false) AS RESULT;";

        CommandDefinition command = new(sql, new
        {
            tipo
        }, cancellationToken: cancellationToken);

        return await _connection.ExecuteScalarAsync<bool>(command);
    }

    public async Task<TIPOS_CONTATOS> GetEmailAsync()
    {
        var sql = "SELECT * FROM TIPOS_CONTATOS WHERE TIPO = 'Email'";

        // Usando QuerySingleOrDefaultAsync para garantir que não haverá exceções se não houver resultados
        return await _connection.QuerySingleOrDefaultAsync<TIPOS_CONTATOS>(sql);
    }

    public async Task<TIPOS_CONTATOS> GetTelefoneAsync()
    {
        var sql = "SELECT * FROM TIPOS_CONTATOS WHERE TIPO = 'Telefone'";

        // Usando QuerySingleOrDefaultAsync para garantir que não haverá exceções se não houver resultados
        return await _connection.QuerySingleOrDefaultAsync<TIPOS_CONTATOS>(sql);
    }
}