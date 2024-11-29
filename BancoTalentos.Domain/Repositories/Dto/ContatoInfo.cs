using BancoTalentos.Domain.Enums;
using BancoTalentos.Domain.Extensions;

namespace BancoTalentos.Domain.Repositories.Dto;

public class ContatoInfo
{
    public string CONTATO { get; set; }
    public int ID_TIPO_CONTATO { get; set; }
    public string DESCRICAO_TIPO_CONTATO { get; set; }
    public DateTime? DATA_INATIVACAO { get; set; }
    public StatusCadastro Status => DATA_INATIVACAO.DataInativacaoToStatus();

    public ContatoInfo(string contato, int id_tipo_contato, string descricao_tipo_contato, DateTime? data_inativacao)
    {
        CONTATO = contato;
        ID_TIPO_CONTATO = id_tipo_contato;
        DESCRICAO_TIPO_CONTATO = descricao_tipo_contato;
        DATA_INATIVACAO = data_inativacao;
    }

    public ContatoInfo()
    {
    }
}

