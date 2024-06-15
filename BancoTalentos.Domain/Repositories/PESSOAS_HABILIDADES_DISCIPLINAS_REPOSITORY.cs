using BancoTalentos.Domain.Repositories.Base;
using BancoTalentos.Domain.Repositories.Contracts.Interfaces;
using System.Data;

namespace BancoTalentos.Domain.Repositories;

public class PESSOAS_HABILIDADES_DISCIPLINAS_REPOSITORY
    : PESSOAS_HABILIDADES_DISCIPLINAS_REPOSITORY_BASE,
        IPESSOAS_HABILIDADES_DISCIPLINAS_REPOSITORY
{
    public PESSOAS_HABILIDADES_DISCIPLINAS_REPOSITORY(IDbConnection _connectionection) : base(_connectionection)
    {
    }
}
