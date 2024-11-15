using BancoTalentos.Domain.Entity.Enums;

namespace BancoTalentos.Domain.Repositories.Dto;

public class HabilidadeInfo
{
    public DateTime DATA_CADASTRO { get; set; }
    public int CARGA_HORARIA_DISCIPLINA { get; set; }
    public string DESCRICAO_DISCIPLINA { get; set; }
    public string NOME_DISCIPLINA { get; set; }
    public CARGO CARGO_PESSOA { get; set; }
}
