using System.Data;
using Dapper;

namespace PWSmartCommerce.DataAccess.Repositories
{
  public abstract class BaseRepository(IDbConnection connection)
  {
    protected async Task<T?> QuerySingleAsync<T>(string sql, object? parameters = null)
    {
      return await connection.QuerySingleOrDefaultAsync<T>(sql, parameters);
    }

    protected async Task<IEnumerable<T>> QueryAsync<T>(string sql, object? parameters = null)
    {
      return await connection.QueryAsync<T>(sql, parameters);
    }

    protected async Task<int> ExecuteAsync(string sql, object? parameters = null)
    {
      return await connection.ExecuteAsync(sql, parameters);
    }
    protected async Task<T?> ExecuteScalarAsync<T>(string sql, object? parameters = null)
    {
      return await connection.ExecuteScalarAsync<T>(sql, parameters);
    }
  }
}