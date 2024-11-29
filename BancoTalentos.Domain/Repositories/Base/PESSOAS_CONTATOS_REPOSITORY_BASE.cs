using BancoTalentos.Domain.Entity;
using BancoTalentos.Domain.Repositories.Base.Shared;
using BancoTalentos.Domain.Repositories.Contracts.Base.Interfaces;
using Dapper;
using System.Data;

// File Auto Generated. DOT NOT MODIFY
namespace BancoTalentos.Domain.Repositories.Base;

public class PESSOAS_CONTATOS_REPOSITORY_BASE : Repository, IPESSOAS_CONTATOS_REPOSITORY_BASE
{
    public PESSOAS_CONTATOS_REPOSITORY_BASE(IDbConnection conn) : base(conn)
    {
    }

    public async Task<int> DeleteAsync(
        PESSOAS_CONTATOS pessoas_contatos,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            object parameters = new { idParam = pessoas_contatos.ID };
            var sql =
                @"DELETE FROM pessoas_contatos
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
        PESSOAS_CONTATOS pessoas_contatos,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            object parameters = new
            {
                contatoParam = pessoas_contatos.CONTATO,
                idtipocontatoParam = pessoas_contatos.ID_TIPO_CONTATO,
                idpessoaParam = pessoas_contatos.ID_PESSOA,
            };
            var sql =
                @"					INSERT INTO pessoas_contatos
					(
					CONTATO
					,ID_TIPO_CONTATO
					,ID_PESSOA
					)
					VALUES
					(
					@contatoParam
					,@idtipocontatoParam
					,@idpessoaParam
					)";

            CommandDefinition command = new(sql, parameters, cancellationToken: cancellationToken);
            var affectedRows = await _connection.ExecuteAsync(command);

            return affectedRows;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<IEnumerable<PESSOAS_CONTATOS>> GetAllAsync(
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            var sql = @"SELECT * FROM pessoas_contatos";

            CommandDefinition command = new(sql, cancellationToken: cancellationToken);
            return await _connection.QueryAsync<PESSOAS_CONTATOS>(command);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<PESSOAS_CONTATOS?> GetByIdAsync(
        object id,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            object parameters = new { idParam = id };
            var sql = @"SELECT * FROM pessoas_contatos WHERE ID = @idParam";

            CommandDefinition command = new(sql, parameters, cancellationToken: cancellationToken);
            return await _connection.QuerySingleOrDefaultAsync<PESSOAS_CONTATOS>(command);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<int> UpdateAsync(
        PESSOAS_CONTATOS pessoas_contatos,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            object parameters = new
            {
                idParam = pessoas_contatos.ID,
                contatoParam = pessoas_contatos.CONTATO,
                idtipocontatoParam = pessoas_contatos.ID_TIPO_CONTATO,
                idpessoaParam = pessoas_contatos.ID_PESSOA,
            };
            var sql =
                @"					UPDATE pessoas_contatos SET
                    CONTATO = @contatoParam
                    ,ID_TIPO_CONTATO = @idtipocontatoParam
                    ,ID_PESSOA = @idpessoaParam
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
