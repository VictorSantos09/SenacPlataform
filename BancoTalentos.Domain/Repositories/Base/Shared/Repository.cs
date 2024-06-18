using BancoTalentos.Domain.Repositories.Contracts.Shared;
using Dapper;
using MySql.Data.MySqlClient;
using System.Data;

/*
File Auto Generated. This file is only generate once.

DO NOT MODIFY THE FILE NAME AND ITS LOCATION
YOU CAN MODIFY THE CLASS METHODS IMPLEMENTATIONS

This class is the base class for all repositories.
When generating the files, this file will be searched for by its name and/or path,
If not found, it will be recreated and you may lose any modifications you have made.
*/

namespace BancoTalentos.Domain.Repositories.Base.Shared;

public abstract class Repository : IRepository
{
    private protected IDbConnection _connection;

    protected Repository(IDbConnection conn)
    {
        _connection = conn;
    }

    private protected IDbTransaction _transaction;

    public IDbConnection Open()
    {
        if (_connection is null || _connection?.State == ConnectionState.Closed)
        {
            _connection.Open();
        }

        return _connection;
    }

    public void Close()
    {
        _connection.Close();
    }

    public void BeginTransaction()
    {
        Open();
        _transaction = _connection.BeginTransaction();
    }

    public void Commit()
    {
        _transaction.Commit();
        Close();
    }

    public void Rollback()
    {
        _transaction.Rollback();
        Close();
    }

    public async Task<bool> ExistsAsync<TKey>(string tableName, TKey id, CancellationToken cancellationToken)
    {
        string sql = $"SELECT IF((SELECT COUNT(1) FROM {tableName} WHERE ID = @Id), true, false) AS RESULT;";

        CommandDefinition command = new(sql, new
        {
            Id = id
        }, cancellationToken: cancellationToken);

        return await _connection.ExecuteScalarAsync<bool>(command);
    }

    public async Task<bool> IfAsync(string sql, object args, CancellationToken cancellationToken)
    {
        var innerSql = @$"SELECT IF((SELECT COUNT(1) FROM {sql}), true, false) AS RESULT";

        CommandDefinition command = new(innerSql, args, cancellationToken: cancellationToken);

        return await _connection.ExecuteScalarAsync<bool>(command);
    }
}
