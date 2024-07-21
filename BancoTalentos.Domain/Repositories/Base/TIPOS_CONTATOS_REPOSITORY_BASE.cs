using BancoTalentos.Domain.Entity;
using BancoTalentos.Domain.Entity.Base;
using BancoTalentos.Domain.Repositories.Base.Shared;
using BancoTalentos.Domain.Repositories.Contracts.Base.Interfaces;
using Dapper;
using System.Data;

// File Auto Generated. DOT NOT MODIFY
namespace BancoTalentos.Domain.Repositories.Base;

public class TIPOS_CONTATOS_REPOSITORY_BASE : Repository, ITIPOS_CONTATOS_REPOSITORY_BASE
{
    public TIPOS_CONTATOS_REPOSITORY_BASE(IDbConnection _connectionection) : base(_connectionection)
    {
    }

    public async Task<int> DeleteAsync(
        TIPOS_CONTATOS tipos_contatos,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            object parameters = new { idParam = tipos_contatos.ID };
            var sql =
                @"DELETE FROM tipos_contatos
WHERE ID = @idParam
";
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
        TIPOS_CONTATOS tipos_contatos,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            object parameters = new
            {
                tipoParam = tipos_contatos.TIPO,
                datacadastroParam = tipos_contatos.DATA_CADASTRO,
                datainativacaoParam = tipos_contatos.DATA_INATIVACAO,
            };
            var sql =
                @"					INSERT INTO tipos_contatos
					(
					TIPO
					,DATA_CADASTRO
					,DATA_INATIVACAO
					)
					VALUES
					(
					@tipoParam
					,@datacadastroParam
					,@datainativacaoParam
					)
";
            CommandDefinition command = new(sql, parameters, cancellationToken: cancellationToken);
            var affectedRows = await _connection.ExecuteAsync(command);

            return affectedRows;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<IEnumerable<TIPOS_CONTATOS>> GetAllAsync(
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            var sql = @"SELECT * FROM tipos_contatos";
            CommandDefinition command = new(sql, cancellationToken: cancellationToken);
            return await _connection.QueryAsync<TIPOS_CONTATOS>(command);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<TIPOS_CONTATOS?> GetByIdAsync(
        object id,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            object parameters = new { idParam = id };
            var sql = @"SELECT * FROM tipos_contatos WHERE ID = @idParam";
            CommandDefinition command = new(sql, parameters, cancellationToken: cancellationToken);
            return await _connection.QuerySingleOrDefaultAsync<TIPOS_CONTATOS>(command);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<int> UpdateAsync(
        TIPOS_CONTATOS tipos_contatos,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            object parameters = new
            {
                idParam = tipos_contatos.ID,
                tipoParam = tipos_contatos.TIPO,
                datacadastroParam = tipos_contatos.DATA_CADASTRO,
                datainativacaoParam = tipos_contatos.DATA_INATIVACAO,
            };
            var sql =
                @"					UPDATE tipos_contatos SET
                    TIPO = @tipoParam
                    ,DATA_CADASTRO = @datacadastroParam
                    ,DATA_INATIVACAO = @datainativacaoParam
                     WHERE ID = @idParam;
";
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
