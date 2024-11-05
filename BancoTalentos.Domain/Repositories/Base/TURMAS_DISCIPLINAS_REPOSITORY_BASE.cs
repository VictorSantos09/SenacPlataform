using BancoTalentos.Domain.Entity;
using BancoTalentos.Domain.Entity.Base;
using BancoTalentos.Domain.Repositories.Base.Shared;
using BancoTalentos.Domain.Repositories.Contracts.Base.Interfaces;
using Dapper;
using System.Data;

// File Auto Generated. DOT NOT MODIFY
namespace BancoTalentos.Domain.Repositories.Base;

public class TURMAS_DISCIPLINAS_REPOSITORY_BASE : Repository, ITURMAS_DISCIPLINAS_REPOSITORY_BASE
{
    public TURMAS_DISCIPLINAS_REPOSITORY_BASE(IDbConnection _connectionection) : base(_connectionection)
    {
    }

    public async Task<int> DeleteAsync(
        TURMAS_DISCIPLINAS turmas_disciplinas,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            object parameters = new { idParam = turmas_disciplinas.ID };
            var sql =
                @"DELETE FROM turmas_disciplinas
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
        TURMAS_DISCIPLINAS turmas_disciplinas,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            object parameters = new
            {
                datainicioParam = turmas_disciplinas.DATA_INICIO,
                datafimParam = turmas_disciplinas.DATA_FIM,
                idprofessorParam = turmas_disciplinas.ID_PROFESSOR,
                idturmaParam = turmas_disciplinas.ID_TURMA,
                iddisciplinaParam = turmas_disciplinas.ID_DISCIPLINA,
            };
            var sql =
                @"					INSERT INTO turmas_disciplinas
					(
					DATA_INICIO
					,DATA_FIM
					,ID_PROFESSOR
					,ID_TURMA
					,ID_DISCIPLINA
					)
					VALUES
					(
					@datainicioParam
					,@datafimParam
					,@idprofessorParam
					,@idturmaParam
					,@iddisciplinaParam
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

    public async Task<IEnumerable<TURMAS_DISCIPLINAS>> GetAllAsync(
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            var sql = @"SELECT * FROM turmas_disciplinas";
            
            CommandDefinition command = new(sql, cancellationToken: cancellationToken);
            return await _connection.QueryAsync<TURMAS_DISCIPLINAS>(command);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<TURMAS_DISCIPLINAS?> GetByIdAsync(
        object id,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            object parameters = new { idParam = id };
            var sql = @"SELECT * FROM turmas_disciplinas WHERE ID = @idParam";
            
            CommandDefinition command = new(sql, parameters, cancellationToken: cancellationToken);
            return await _connection.QuerySingleOrDefaultAsync<TURMAS_DISCIPLINAS>(command);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<int> UpdateAsync(
        TURMAS_DISCIPLINAS turmas_disciplinas,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            object parameters = new
            {
                idParam = turmas_disciplinas.ID,
                datainicioParam = turmas_disciplinas.DATA_INICIO,
                datafimParam = turmas_disciplinas.DATA_FIM,
                idprofessorParam = turmas_disciplinas.ID_PROFESSOR,
                idturmaParam = turmas_disciplinas.ID_TURMA,
                iddisciplinaParam = turmas_disciplinas.ID_DISCIPLINA,
            };
            var sql =
                @"					UPDATE turmas_disciplinas SET
                    DATA_INICIO = @datainicioParam
                    ,DATA_FIM = @datafimParam
                    ,ID_PROFESSOR = @idprofessorParam
                    ,ID_TURMA = @idturmaParam
                    ,ID_DISCIPLINA = @iddisciplinaParam
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
