using BancoTalentos.Domain.Repositories.Base;
using BancoTalentos.Domain.Repositories.Contracts.Interfaces;
using System.Data;

namespace BancoTalentos.Domain.Repositories;

public class FORMACOES_REPOSITORY : FORMACOES_REPOSITORY_BASE, IFORMACOES_REPOSITORY
{
    public FORMACOES_REPOSITORY(IDbConnection conn) : base(conn)
    {
    }
}
