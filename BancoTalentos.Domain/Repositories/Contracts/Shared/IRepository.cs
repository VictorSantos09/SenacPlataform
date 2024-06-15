using System.Data;
using System.Transactions;

namespace BancoTalentos.Domain.Repositories.Contracts.Shared;

public interface IRepository
{
    IDbConnection Open();
    void BeginTransaction();
    void Close();
    void Commit();
    void Rollback();
    Task<bool> ExistsAsync<TKey>(string tableName, TKey id, CancellationToken cancellationToken);
}
