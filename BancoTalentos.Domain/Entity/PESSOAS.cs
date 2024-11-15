using BancoTalentos.Domain.Entity.Base;
using BancoTalentos.Domain.Repositories.Dto;

namespace BancoTalentos.Domain.Entity;

public class PESSOAS : PESSOAS_BASE
{
    public IEnumerable<ContatoInfo> Contatos { get; set; }
    public IEnumerable<DISCIPLINAS> Habilidades { get; set; }
}
