using BancoTalentos.Domain.Entity;
using BancoTalentos.Domain.Entity.Enums;
using BancoTalentos.Domain.Repositories.Base;
using BancoTalentos.Domain.Repositories.Contracts.Interfaces;
using BancoTalentos.Domain.Repositories.Dto;
using Dapper;
using System.Data;

namespace BancoTalentos.Domain.Repositories;

public class PESSOAS_REPOSITORY : PESSOAS_REPOSITORY_BASE, IPESSOAS_REPOSITORY
{
    public PESSOAS_REPOSITORY(IDbConnection conn) : base(conn)
    {
    }

    public async Task<IEnumerable<PESSOAS>> GetAllByCargoAsync(CARGO cargo, CancellationToken cancellationToken = default)
    {
        var sql = $"SELECT * FROM PESSOAS WHERE CARGO = '{cargo}'";

        CommandDefinition command = new(sql, cancellationToken: cancellationToken);
        return await _connection.QueryAsync<PESSOAS>(command);
    }

    public async Task<IEnumerable<ContatoInfo>> BuscaContatosInfo(int idPessoa)
    {
        var sql = @$"select pc.CONTATO {nameof(ContatoInfo.CONTATO)}
		                    , tc.ID AS ID_TIPO_CONTATO {nameof(ContatoInfo.ID_TIPO_CONTATO)}
                            , tc.TIPO AS DESCRICAO_TIPO_CONTATO {nameof(ContatoInfo.DESCRICAO_TIPO_CONTATO)}
                            , tc.DATA_INATIVACAO {nameof(ContatoInfo.DATA_INATIVACAO)}
                    from pessoas_contatos pc
                    join pessoas p on p.ID = pc.ID_PESSOA
                    join tipos_contatos tc on tc.ID = pc.ID_TIPO_CONTATO
                    where p.ID = @{idPessoa}";

        CommandDefinition command = new(sql, new {idPessoa});
        return await _connection.QueryAsync<ContatoInfo>(command);
    }

    public async Task<IEnumerable<HabilidadeInfo>> BuscaHabilidades(int idPessoa)
    {
        var sql = @$"select phd.DATA_CADASTRO {nameof(HabilidadeInfo.DATA_CADASTRO)}
		                    , d.CARGA_HORARIA {nameof(HabilidadeInfo.CARGA_HORARIA_DISCIPLINA)}
                            , d.DESCRICAO {nameof(HabilidadeInfo.DESCRICAO_DISCIPLINA)}
                            , d.NOME {nameof(HabilidadeInfo.NOME_DISCIPLINA)}
                            , p.CARGO {nameof(HabilidadeInfo.CARGO_PESSOA)}
                    from pessoas_habilidades_disciplinas phd
                    join pessoas p on p.ID = phd.ID_PESSOA
                    join disciplinas d on d.ID = phd.ID_DISCIPLINA
                    where p.ID = @{nameof(idPessoa)}";

        CommandDefinition command = new(sql, new { idPessoa });
        return await _connection.QueryAsync<HabilidadeInfo>(command);
    }

    public async Task<PESSOAS?> GetByIdAsync(int idPessoa, CancellationToken cancellationToken = default)
    {
        var sql = "SELECT * FROM PESSOAS WHERE ID = @IdPessoa"; 

        CommandDefinition command = new(sql, new { IdPessoa = idPessoa }, cancellationToken: cancellationToken);
        var pessoa = await _connection.QueryFirstOrDefaultAsync<PESSOAS>(command);
        return pessoa;
    }

    public async Task<bool> AtualizarAsync(PESSOAS pessoa, CancellationToken cancellationToken = default)
    {
        var sql = @"UPDATE PESSOAS
                        SET NOME = @Nome,
                            FOTO = @Foto,
                            CARGO = @Cargo,
                            CARGA_HORARIA = @CargaHoraria
                        WHERE ID = @IdPessoa"; 

        CommandDefinition command = new(sql, new
        {
            pessoa.NOME,
            pessoa.FOTO,
            pessoa.CARGO,
            pessoa.CARGA_HORARIA,
            pessoa.ID // Utiliza o ID para a cláusula WHERE
        }, cancellationToken: cancellationToken);

        var result = await _connection.ExecuteAsync(command); // ExecuteAsync retorna o número de linhas afetadas
        return result > 0; // Se o número de linhas afetadas for maior que 0, a atualização foi bem-sucedida
    }
}
