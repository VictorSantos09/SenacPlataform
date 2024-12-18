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
        var sql = @$"SELECT pc.CONTATO {nameof(ContatoInfo.CONTATO)}
                        , tc.ID AS {nameof(ContatoInfo.ID_TIPO_CONTATO)}
                        , tc.TIPO AS {nameof(ContatoInfo.DESCRICAO_TIPO_CONTATO)}
                        , tc.DATA_INATIVACAO {nameof(ContatoInfo.DATA_INATIVACAO)}
                    FROM pessoas_contatos pc
                    JOIN pessoas p ON p.ID = pc.ID_PESSOA
                    JOIN tipos_contatos tc ON tc.ID = pc.ID_TIPO_CONTATO
                    WHERE p.ID = @{nameof(idPessoa)}";

        CommandDefinition command = new(sql, new { idPessoa });

        var result = await _connection.QueryAsync<ContatoInfo>(command);
        if (result == null || !result.Any())
        {
            Console.WriteLine($"Nenhum contato encontrado para o idPessoa: {idPessoa}");
        }
        return result;
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
}
