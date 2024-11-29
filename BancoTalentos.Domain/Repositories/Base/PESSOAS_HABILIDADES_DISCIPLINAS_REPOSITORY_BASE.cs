using BancoTalentos.Domain.Entity;
using BancoTalentos.Domain.Repositories.Base.Shared;
using BancoTalentos.Domain.Repositories.Contracts.Base.Interfaces;
using Dapper;
using System.Data;

// File Auto Generated. DOT NOT MODIFY
namespace BancoTalentos.Domain.Repositories.Base;

public class PESSOAS_HABILIDADES_DISCIPLINAS_REPOSITORY_BASE
    : Repository,
        IPESSOAS_HABILIDADES_DISCIPLINAS_REPOSITORY_BASE
{
    public PESSOAS_HABILIDADES_DISCIPLINAS_REPOSITORY_BASE(IDbConnection _connectionection) : base(_connectionection)
    {
    }

    public async Task<int> DeleteAsync(
        PESSOAS_HABILIDADES_DISCIPLINAS pessoas_habilidades_disciplinas,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            object parameters = new { idParam = pessoas_habilidades_disciplinas.ID };
            var sql =
                @"DELETE FROM pessoas_habilidades_disciplinas
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
        PESSOAS_HABILIDADES_DISCIPLINAS pessoas_habilidades_disciplinas,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            object parameters = new
            {
                idpessoaParam = pessoas_habilidades_disciplinas.ID_PESSOA,
                iddisciplinaParam = pessoas_habilidades_disciplinas.ID_DISCIPLINA,
                datacadastroParam = pessoas_habilidades_disciplinas.DATA_CADASTRO,
            };
            var sql =
                @"					INSERT INTO pessoas_habilidades_disciplinas
					(
					ID_PESSOA
					,ID_DISCIPLINA
					,DATA_CADASTRO
					)
					VALUES
					(
					@idpessoaParam
					,@iddisciplinaParam
					,@datacadastroParam
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

    public async Task<IEnumerable<PESSOAS_HABILIDADES_DISCIPLINAS>> GetAllAsync(
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            var sql = @"SELECT * FROM pessoas_habilidades_disciplinas";

            CommandDefinition command = new(sql, cancellationToken: cancellationToken);
            return await _connection.QueryAsync<PESSOAS_HABILIDADES_DISCIPLINAS>(command);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<PESSOAS_HABILIDADES_DISCIPLINAS?> GetByIdAsync(
        object id,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            object parameters = new { idParam = id };
            var sql = @"SELECT * FROM pessoas_habilidades_disciplinas WHERE ID = @idParam";

            CommandDefinition command = new(sql, parameters, cancellationToken: cancellationToken);
            return await _connection.QuerySingleOrDefaultAsync<PESSOAS_HABILIDADES_DISCIPLINAS>(
                command
            );
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<int> UpdateAsync(
        PESSOAS_HABILIDADES_DISCIPLINAS pessoas_habilidades_disciplinas,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            object parameters = new
            {
                idParam = pessoas_habilidades_disciplinas.ID,
                idpessoaParam = pessoas_habilidades_disciplinas.ID_PESSOA,
                iddisciplinaParam = pessoas_habilidades_disciplinas.ID_DISCIPLINA,
                datacadastroParam = pessoas_habilidades_disciplinas.DATA_CADASTRO,
            };
            var sql =
                @"					UPDATE pessoas_habilidades_disciplinas SET
                    ID_PESSOA = @idpessoaParam
                    ,ID_DISCIPLINA = @iddisciplinaParam
                    ,DATA_CADASTRO = @datacadastroParam
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
