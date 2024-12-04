using BancoTalentos.Domain.Entities;
using BancoTalentos.Domain.Entities.Base;
using BancoTalentos.Domain.Repositories.Base.Interfaces;
using BancoTalentos.Domain.Repositories.Base.Shared;
using Dapper;
using System.Data;

// File Auto Generated. DOT NOT MODIFY
namespace BancoTalentos.Domain.Repositories.Base;

public class FORMACOES_REPOSITORY_BASE : Repository, IFORMACOES_REPOSITORY_BASE
{
    public FORMACOES_REPOSITORY_BASE(IDbConnection conn) : base(conn)
    {
    }

    public async Task<int> DeleteAsync(
        FORMACOES formacoes,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            object parameters = new { idParam = formacoes.ID };
            var sql =
                @"DELETE FROM formacoes
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
        FORMACOES formacoes,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            object parameters = new
            {
                nomeParam = formacoes.NOME,
                datacadastroParam = formacoes.DATA_CADASTRO,
                datainativacaoParam = formacoes.DATA_INATIVACAO,
            };
            var sql =
                @"					INSERT INTO formacoes
					(
					NOME
					,DATA_CADASTRO
					,DATA_INATIVACAO
					)
					VALUES
					(
					@nomeParam
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

    public async Task<IEnumerable<FORMACOES>> GetAllAsync(
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            var sql = @"SELECT * FROM formacoes";

            CommandDefinition command = new(sql, cancellationToken: cancellationToken);
            return await _connection.QueryAsync<FORMACOES>(command);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<FORMACOES?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            object parameters = new { idParam = id };
            var sql = @"SELECT * FROM formacoes WHERE ID = @idParam";

            CommandDefinition command = new(sql, parameters, cancellationToken: cancellationToken);
            return await _connection.QuerySingleOrDefaultAsync<FORMACOES>(command);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<int> UpdateAsync(
        FORMACOES formacoes,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            object parameters = new
            {
                idParam = formacoes.ID,
                nomeParam = formacoes.NOME,
                datacadastroParam = formacoes.DATA_CADASTRO,
                datainativacaoParam = formacoes.DATA_INATIVACAO,
            };
            var sql =
                @"					UPDATE formacoes SET
                    NOME = @nomeParam
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
