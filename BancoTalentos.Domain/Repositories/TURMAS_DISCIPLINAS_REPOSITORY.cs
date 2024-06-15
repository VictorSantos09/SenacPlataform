using BancoTalentos.Domain.Repositories.Base;
using BancoTalentos.Domain.Repositories.Contracts.Interfaces;
using System.Data;

namespace BancoTalentos.Domain.Repositories;

public class TURMAS_DISCIPLINAS_REPOSITORY
    : TURMAS_DISCIPLINAS_REPOSITORY_BASE,
        ITURMAS_DISCIPLINAS_REPOSITORY
{
    public TURMAS_DISCIPLINAS_REPOSITORY(IDbConnection _connectionection) : base(_connectionection)
    {
    }
}
