using BancoTalentos.Domain.Entity;
using BancoTalentos.Domain.Repositories.Base;
using BancoTalentos.Domain.Repositories.Contracts.Interfaces;
using Dapper;
using System.Data;

namespace BancoTalentos.Domain.Repositories;

public class PESSOAS_CONTATOS_REPOSITORY
    : PESSOAS_CONTATOS_REPOSITORY_BASE,
        IPESSOAS_CONTATOS_REPOSITORY
{
    public PESSOAS_CONTATOS_REPOSITORY(IDbConnection _connectionection) : base(_connectionection)
    {
    }

    public async Task<IEnumerable<PESSOAS_CONTATOS>> GetByIdPessoaAsync(int idPessoa, CancellationToken cancellationToken = default)
    {
        var sql = @$"SELECT * 
                    FROM PESSOAS_CONTATOS
                    WHERE ID_PESSOA = @idPessoa";

        CommandDefinition command = new(sql, new { idPessoa }, cancellationToken: cancellationToken);
        return await _connection.QueryAsync<PESSOAS_CONTATOS>(command);
    }

    public async Task<bool> HasContatoCadadastradoAsync(string contato, int idPessoa, CancellationToken cancellationToken = default)
    {
        var sql = @$"PESSOAS_CONTATOS
                WHERE CONTATO = @contato
                AND ID_PESSOA = @idPessoa";

        return await IfAsync(sql, new
        {
            contato,
            idPessoa
        }, cancellationToken);
    }

    public async Task<bool> HasPessoaComTipoContatoAsync(int idTipo, CancellationToken cancellationToken)
    {
        var sql = @"SELECT ID FROM PESSOAS_CONTATOS WHERE ID_TIPO_CONTATO = @idTipo";

        return await IfAsync(sql, new { idTipo }, cancellationToken);
    }
}
