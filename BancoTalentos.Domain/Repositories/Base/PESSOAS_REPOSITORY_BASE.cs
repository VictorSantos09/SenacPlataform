using BancoTalentos.Domain.Entity;
using BancoTalentos.Domain.Entity.Base;
using BancoTalentos.Domain.Repositories.Base.Shared;
using BancoTalentos.Domain.Repositories.Contracts.Base.Interfaces;
using Dapper;
using System.Data;

// File Auto Generated. DOT NOT MODIFY
namespace BancoTalentos.Domain.Repositories.Base;

public class PESSOAS_REPOSITORY_BASE : Repository, IPESSOAS_REPOSITORY_BASE
{
    public PESSOAS_REPOSITORY_BASE(IDbConnection _connectionection) : base(_connectionection)
    {
    }

    public async Task<int> DeleteAsync(
        PESSOAS pessoas,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            object parameters = new { idParam = pessoas.ID };
            var sql =
                @"DELETE FROM pessoas
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
        PESSOAS pessoas,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            object parameters = new
            {
                nomeParam = pessoas.NOME,
                fotoParam = pessoas.FOTO,
                cargoParam = pessoas.CARGO.ToString(),
                cargahorariaParam = pessoas.CARGA_HORARIA,
            };
            var sql =
                @"					INSERT INTO pessoas
					(
					NOME
					,FOTO
					,CARGO
					,CARGA_HORARIA
					)
					VALUES
					(
					@nomeParam
					,@fotoParam
					,@cargoParam
					,@cargahorariaParam
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

    public async Task<IEnumerable<PESSOAS>> GetAllAsync(
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            var sql = @"SELECT * FROM pessoas";
            
            CommandDefinition command = new(sql, cancellationToken: cancellationToken);
            return await _connection.QueryAsync<PESSOAS>(command);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<PESSOAS?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            object parameters = new { idParam = id };
            var sql = @"SELECT * FROM pessoas WHERE ID = @idParam";

            CommandDefinition command = new(sql, parameters, cancellationToken: cancellationToken);
            return await _connection.QuerySingleOrDefaultAsync<PESSOAS>(command);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<int> UpdateAsync(
        PESSOAS pessoas,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            object parameters = new
            {
                idParam = pessoas.ID,
                nomeParam = pessoas.NOME,
                fotoParam = pessoas.FOTO,
                cargoParam = pessoas.CARGO.ToString(),
                cargahorariaParam = pessoas.CARGA_HORARIA,
            };
            var sql =
                @"					UPDATE pessoas SET
                    NOME = @nomeParam
                    ,FOTO = @fotoParam
                    ,CARGO = @cargoParam
                    ,CARGA_HORARIA = @cargahorariaParam
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

    public async Task<int> GetMaxIdAsync()
    {
        return await _connection.QuerySingleAsync<int>("SELECT MAX(ID) FROM PESSOAS");
    }
}
