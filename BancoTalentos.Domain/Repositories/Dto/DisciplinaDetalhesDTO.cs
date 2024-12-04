using BancoTalentos.Domain.Entity.Enums;

namespace BancoTalentos.Domain.Repositories.Dto;

public class DisciplinaDetalhesDTO
{
    public int CARGA_HORARIA_PESSOA { get; set; }
    public CARGO CARGO_PESSOA { get; set; }
    //public string? CAMINHO_FOTO_PESSOA { get; set; }
    public string NOME_PESSOA { get; set; }
    public DateTime DATA_CADASTRO { get; set; }
}
