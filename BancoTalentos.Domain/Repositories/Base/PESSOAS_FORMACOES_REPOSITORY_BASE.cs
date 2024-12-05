using BancoTalentos.Domain.Entities;
using BancoTalentos.Domain.Entities.Base;
using BancoTalentos.Domain.Repositories.Base.Interfaces;
using BancoTalentos.Domain.Repositories.Base.Shared;
using Dapper;
using System.Data;

// File Auto Generated. DOT NOT MODIFY
namespace BancoTalentos.Domain.Repositories.Base;

public class PESSOAS_FORMACOES_REPOSITORY_BASE : Repository, IPESSOAS_FORMACOES_REPOSITORY_BASE
{
    public PESSOAS_FORMACOES_REPOSITORY_BASE(IDbConnection _connectionection) : base(_connectionection)
    {
    }

    public async Task<int> DeleteAsync(
    PESSOAS_FORMACOES pessoas_formacoes,
    CancellationToken cancellationToken = default
)
    {
        try
        {
            object parameters = new { idParam = pessoas_formacoes.ID };
            var sql =
                @"DELETE FROM pessoas_formacoes
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
        PESSOAS_FORMACOES pessoas_formacoes,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            object parameters = new
            {
                idpessoaParam = pessoas_formacoes.ID_PESSOA,
                idformacaoParam = pessoas_formacoes.ID_FORMACAO,
                datacadastroParam = pessoas_formacoes.DATA_CADASTRO,
                datainativacaoParam = pessoas_formacoes.DATA_INATIVACAO,
                inicioParam = pessoas_formacoes.INICIO,
                fimParam = pessoas_formacoes.FIM,
                tipoformacaoParam = pessoas_formacoes.TIPO_FORMACAO,
                modeloensinoParam = pessoas_formacoes.MODELO_ENSINO,
            };
            var sql =
                @"INSERT INTO pessoas_formacoes
					(
					ID_PESSOA
					,ID_FORMACAO
					,DATA_CADASTRO
					,DATA_INATIVACAO
					,INICIO
					,FIM
					,TIPO_FORMACAO
					,MODELO_ENSINO
					)
					VALUES
					(
					@idpessoaParam
					,@idformacaoParam
					,@datacadastroParam
					,@datainativacaoParam
					,@inicioParam
					,@fimParam
					,@tipoformacaoParam
					,@modeloensinoParam
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

    public async Task<IEnumerable<PESSOAS_FORMACOES>> GetAllAsync(
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            var sql = @"SELECT * FROM pessoas_formacoes";

            CommandDefinition command = new(sql, cancellationToken: cancellationToken);
            return await _connection.QueryAsync<PESSOAS_FORMACOES>(command);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<PESSOAS_FORMACOES?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            object parameters = new { idParam = id };
            var sql = @"SELECT * FROM pessoas_formacoes WHERE ID = @idParam";

            CommandDefinition command = new(sql, parameters, cancellationToken: cancellationToken);
            return await _connection.QuerySingleOrDefaultAsync<PESSOAS_FORMACOES>(command);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<int> UpdateAsync(
        PESSOAS_FORMACOES pessoas_formacoes,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            object parameters = new
            {
                idParam = pessoas_formacoes.ID,
                idpessoaParam = pessoas_formacoes.ID_PESSOA,
                idformacaoParam = pessoas_formacoes.ID_FORMACAO,
                datacadastroParam = pessoas_formacoes.DATA_CADASTRO,
                datainativacaoParam = pessoas_formacoes.DATA_INATIVACAO,
                inicioParam = pessoas_formacoes.INICIO,
                fimParam = pessoas_formacoes.FIM,
                tipoformacaoParam = pessoas_formacoes.TIPO_FORMACAO,
                modeloensinoParam = pessoas_formacoes.MODELO_ENSINO,
            };
            var sql =
                @"					UPDATE pessoas_formacoes SET
                    ID_PESSOA = @idpessoaParam
                    ,ID_FORMACAO = @idformacaoParam
                    ,DATA_CADASTRO = @datacadastroParam
                    ,DATA_INATIVACAO = @datainativacaoParam
                    ,INICIO = @inicioParam
                    ,FIM = @fimParam
                    ,TIPO_FORMACAO = @tipoformacaoParam
                    ,MODELO_ENSINO = @modeloensinoParam
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
