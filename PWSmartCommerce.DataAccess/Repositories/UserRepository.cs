using System.Data;
using PWSmartCommerce.Domain.Models;

namespace PWSmartCommerce.DataAccess.Repositories
{
  public class UserRepository(IDbConnection connection) : BaseRepository(connection)
  {
    public async Task<User?> GetByUsernameAsync(string username)
    {
      const string sql = "SELECT * FROM Users WHERE Username = @Username";
      return await QuerySingleAsync<User>(sql, new { Username = username });
    }

    public async Task AddAsync(User user)
    {
      const string sql = "INSERT INTO Users (Username, PasswordHash, Email, CreatedAt) VALUES (@Username, @PasswordHash, @Email, @CreatedAt)";
      await ExecuteAsync(sql, user);
    }
  }
}