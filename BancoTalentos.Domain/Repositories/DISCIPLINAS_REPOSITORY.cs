using BancoTalentos.Domain.Repositories.Base;
using BancoTalentos.Domain.Repositories.Contracts.Interfaces;
using System.Data;

namespace BancoTalentos.Domain.Repositories;

public class DISCIPLINAS_REPOSITORY : DISCIPLINAS_REPOSITORY_BASE, IDISCIPLINAS_REPOSITORY
{
    public DISCIPLINAS_REPOSITORY(IDbConnection _connectionection) : base(_connectionection)
    {
    }
}
