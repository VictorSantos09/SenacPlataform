using BancoTalentos.Domain.Entity;
using BancoTalentos.Domain.Entity.Base;
using BancoTalentos.Domain.Repositories.Base.Shared;
using BancoTalentos.Domain.Repositories.Contracts.Base.Interfaces;
using Dapper;
using System.Data;

// File Auto Generated. DOT NOT MODIFY
namespace BancoTalentos.Domain.Repositories.Base;

public class DISCIPLINAS_REPOSITORY_BASE : Repository, IDISCIPLINAS_REPOSITORY_BASE
{
    public DISCIPLINAS_REPOSITORY_BASE(IDbConnection _connectionection) : base(_connectionection)
    {
    }

    public async Task<int> DeleteAsync(
        DISCIPLINAS disciplinas,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            object parameters = new { idParam = disciplinas.ID };
            var sql =
                @"DELETE FROM disciplinas
WHERE ID = @idParam
";
            using var _connection = Open();
            CommandDefinition command = new(sql, parameters, cancellationToken: cancellationToken);
            var affectedRows = await _connection.ExecuteAsync(command);

            return affectedRows;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<int> InsertAsync(
        DISCIPLINAS disciplinas,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            object parameters = new
            {
                nomeParam = disciplinas.NOME,
                cargahorariaParam = disciplinas.CARGA_HORARIA,
                descricaoParam = disciplinas.DESCRICAO,
            };
            var sql =
                @"					INSERT INTO disciplinas
					(
					NOME
					,CARGA_HORARIA
					,DESCRICAO
					)
					VALUES
					(
					@nomeParam
					,@cargahorariaParam
					,@descricaoParam
					)
";
            using var _connection = Open();
            CommandDefinition command = new(sql, parameters, cancellationToken: cancellationToken);
            var affectedRows = await _connection.ExecuteAsync(command);

            return affectedRows;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<IEnumerable<DISCIPLINAS>> GetAllAsync(
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            var sql = @"SELECT * FROM disciplinas";
            using var _connection = Open();
            CommandDefinition command = new(sql, cancellationToken: cancellationToken);
            return await _connection.QueryAsync<DISCIPLINAS>(command);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<DISCIPLINAS?> GetByIdAsync(
        object id,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            object parameters = new { idParam = id };
            var sql = @"SELECT * FROM disciplinas WHERE ID = @idParam";
            using var _connection = Open();
            CommandDefinition command = new(sql, parameters, cancellationToken: cancellationToken);
            return await _connection.QuerySingleOrDefaultAsync<DISCIPLINAS>(command);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<int> UpdateAsync(
        DISCIPLINAS disciplinas,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            object parameters = new
            {
                idParam = disciplinas.ID,
                nomeParam = disciplinas.NOME,
                cargahorariaParam = disciplinas.CARGA_HORARIA,
                descricaoParam = disciplinas.DESCRICAO,
            };
            var sql =
                @"					UPDATE disciplinas SET
                    NOME = @nomeParam
                    ,CARGA_HORARIA = @cargahorariaParam
                    ,DESCRICAO = @descricaoParam
                     WHERE ID = @idParam;
";
            using var _connection = Open();
            CommandDefinition command = new(sql, parameters, cancellationToken: cancellationToken);
            var affectedRows = await _connection.ExecuteAsync(command);

            return affectedRows;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
