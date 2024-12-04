namespace BancoTalentos.Domain.Entities.Base;

// File Auto Generated. DOT NOT MODIFY
public class PESSOAS_FORMACOES_BASE
{
    public int ID { get; set; }
    public int ID_PESSOA { get; set; }
    public int ID_FORMACAO { get; set; }
    public DateTime DATA_CADASTRO { get; set; }
    public DateTime? DATA_INATIVACAO { get; set; }
    public DateTime INICIO { get; set; }
    public DateTime? FIM { get; set; }
    public TIPO_FORMACAO TIPO_FORMACAO { get; set; }
    public MODELO_ENSINO MODELO_ENSINO { get; set; }
}
