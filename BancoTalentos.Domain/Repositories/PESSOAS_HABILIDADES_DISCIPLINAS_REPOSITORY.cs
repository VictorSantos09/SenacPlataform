using BancoTalentos.Domain.Entity;
using BancoTalentos.Domain.Repositories.Base;
using BancoTalentos.Domain.Repositories.Contracts.Interfaces;
using Dapper;
using System.Data;

namespace BancoTalentos.Domain.Repositories;

public class PESSOAS_HABILIDADES_DISCIPLINAS_REPOSITORY
    : PESSOAS_HABILIDADES_DISCIPLINAS_REPOSITORY_BASE,
        IPESSOAS_HABILIDADES_DISCIPLINAS_REPOSITORY
{
    public PESSOAS_HABILIDADES_DISCIPLINAS_REPOSITORY(IDbConnection _connectionection) : base(_connectionection)
    {
    }

    public async Task<IEnumerable<PESSOAS_HABILIDADES_DISCIPLINAS>> GetByIdPessoaAsync(int idPessoa, CancellationToken cancellationToken = default)
    {
        var sql = @$"SELECT * 
                    FROM PESSOAS_CONTATOS
                    WHERE ID_PESSOA = @idPessoa";

        CommandDefinition command = new(sql, new { idPessoa }, cancellationToken: cancellationToken);
        return await _connection.QueryAsync<PESSOAS_HABILIDADES_DISCIPLINAS>(command);
    }

    public async Task<bool> HasHabilidadeCadastrada(int idDisciplina, int idPessoa, CancellationToken cancellationToken = default)
    {
        var sql = @"PESSOAS_HABILIDADES_DISCIPLINAS
                    WHERE ID_PESSOA = @idPessoa
                    AND ID_DISCIPLINA = @idDisciplina";

        return await IfAsync(sql, new
        {
            idPessoa,
            idDisciplina
        }, cancellationToken);
    }
}
