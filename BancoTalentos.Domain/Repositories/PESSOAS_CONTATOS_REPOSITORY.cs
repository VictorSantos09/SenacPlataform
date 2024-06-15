using BancoTalentos.Domain.Repositories.Base;
using BancoTalentos.Domain.Repositories.Contracts.Interfaces;
using System.Data;

namespace BancoTalentos.Domain.Repositories;

public class PESSOAS_CONTATOS_REPOSITORY
    : PESSOAS_CONTATOS_REPOSITORY_BASE,
        IPESSOAS_CONTATOS_REPOSITORY
{
    public PESSOAS_CONTATOS_REPOSITORY(IDbConnection _connectionection) : base(_connectionection)
    {
    }
}
