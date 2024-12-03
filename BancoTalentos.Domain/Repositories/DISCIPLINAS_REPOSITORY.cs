using BancoTalentos.Domain.Entity;
using BancoTalentos.Domain.Repositories.Base;
using BancoTalentos.Domain.Repositories.Contracts.Interfaces;
using BancoTalentos.Domain.Repositories.Dto;
using Dapper;
using System.Data;

namespace BancoTalentos.Domain.Repositories;

public class DISCIPLINAS_REPOSITORY : DISCIPLINAS_REPOSITORY_BASE, IDISCIPLINAS_REPOSITORY
{
    public DISCIPLINAS_REPOSITORY(IDbConnection _connectionection) : base(_connectionection)
    {
    }

    public async Task<bool> ExistsBy_IDX_DISCIPLINAS_001_Async(string nome, CancellationToken cancellationToken)
    {
        var query = "DISCIPLINAS WHERE NOME = @nome";

        return await IfAsync(query, new { nome }, cancellationToken);
    }

    public async Task<DISCIPLINAS?> GetByNameAsync(string nome, CancellationToken cancellationToken)
    {
        var query = "SELECT * FROM DISCIPLINAS WHERE NOME = @nome";

        CommandDefinition command = new(query, new { nome }, cancellationToken: cancellationToken);
        return await _connection.QuerySingleOrDefaultAsync(command);
    }

    public async Task<IEnumerable<DISCIPLINAS>> GetAll_DetalhadoAsync()
    {
        var sql = @"SELECT d.*,
                   COUNT(phd.ID_DISCIPLINA) AS QTD_PESSOAS_CAPACITADAS
            FROM disciplinas d
            LEFT JOIN pessoas_habilidades_disciplinas phd ON phd.ID_DISCIPLINA = d.ID
            GROUP BY d.ID"
        ;

        CommandDefinition command = new(sql);
        return await _connection.QueryAsync<DISCIPLINAS>(command);
    }

    public async Task<IEnumerable<DisciplinaDetalhesDTO>> GetDetalhesPessoasHabilitadas(int id)
    {
        var sql = @"select p.CARGA_HORARIA CARGA_HORARIA_PESSOA
		                    , p.CARGO CARGO_PESSOA
                            , p.FOTO CAMINHO_FOTO_PESSOA
                            , p.NOME NOME_PESSOA
                            , phd.DATA_CADASTRO
                    from disciplinas d
                    join pessoas_habilidades_disciplinas phd on phd.ID_DISCIPLINA = d.ID
                    join pessoas p on p.ID = phd.ID_PESSOA
                    where d.ID = @id";

        return await _connection.QueryAsync<DisciplinaDetalhesDTO>(sql, new { id });
    }
}
