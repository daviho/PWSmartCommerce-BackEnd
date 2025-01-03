using System.Data;
using PWSmartCommerce.Domain.Models;

namespace PWSmartCommerce.DataAccess.Repositories
{
  public class TenantRepository(IDbConnection connection) : BaseRepository(connection)
  {
    public async Task<IEnumerable<Tenant>> GetAllAsync()
    {
      const string sql = "SELECT * FROM Tenants";
      return await QueryAsync<Tenant>(sql);
    }

    public async Task<Tenant?> GetByIdAsync(int tenantId)
    {
      const string sql = "SELECT * FROM Tenants WHERE TenantID = @TenantID";
      return await QuerySingleAsync<Tenant>(sql, new { TenantID = tenantId });
    }

    public async Task<int> CreateAsync(Tenant tenant)
    {
      const string sql = "INSERT INTO Tenants (Name, TaxID, Address, City, State, Country, CreatedAt, UpdatedAt) " +
                         "VALUES (@Name, @TaxID, @Address, @City, @State, @Country, @CreatedAt, @UpdatedAt) " +
                         "SELECT CAST(SCOPE_IDENTITY() as int)";
      return await ExecuteScalarAsync<int>(sql, tenant);
    }

    public async Task<int> UpdateAsync(Tenant tenant)
    {
      const string sql = "UPDATE Tenants SET " +
                         "Name = @Name, " +
                         "TaxID = @TaxID, " +
                         "Address = @Address, " +
                         "City = @City, " +
                         "State = @State, " +
                         "Country = @Country, " +
                         "UpdatedAt = @UpdatedAt " +
                         "WHERE TenantID = @TenantID";
      return await ExecuteAsync(sql, tenant);
    }

    public async Task<bool> DeleteAsync(int tenantId)
    {
      const string sql = "DELETE FROM Tenants WHERE TenantID = @TenantID";
      var rowsAffected = await ExecuteAsync(sql, new { TenantID = tenantId });
      return rowsAffected > 0;
    }
  }
}