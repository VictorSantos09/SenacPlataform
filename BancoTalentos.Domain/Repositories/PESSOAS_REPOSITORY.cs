using BancoTalentos.Domain.Repositories.Base;
using BancoTalentos.Domain.Repositories.Contracts.Interfaces;
using System.Data;

namespace BancoTalentos.Domain.Repositories;

public class PESSOAS_REPOSITORY : PESSOAS_REPOSITORY_BASE, IPESSOAS_REPOSITORY
{
    public PESSOAS_REPOSITORY(IDbConnection _connectionection) : base(_connectionection)
    {
    }
}
