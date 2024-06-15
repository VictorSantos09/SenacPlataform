using BancoTalentos.Domain.Repositories.Base;
using BancoTalentos.Domain.Repositories.Contracts.Interfaces;
using System.Data;

namespace BancoTalentos.Domain.Repositories;

public class TIPOS_CONTATOS_REPOSITORY
    : TIPOS_CONTATOS_REPOSITORY_BASE,
        ITIPOS_CONTATOS_REPOSITORY
{
    public TIPOS_CONTATOS_REPOSITORY(IDbConnection _connectionection) : base(_connectionection)
    {
    }
}
