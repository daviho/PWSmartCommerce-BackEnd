using PWSmartCommerce.DataAccess.Repositories;
using PWSmartCommerce.Domain.DTOs.Login;
using PWSmartCommerce.Services.Interfaces;
using PWSmartCommerce.Services.Utils;

namespace PWSmartCommerce.Services.Implementations
{
  public class AuthService(UserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator) : IAuthService
  {
    public async Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequest)
    {
      var user = await userRepository.GetByUsernameAsync(loginRequest.UserName);

      if (user == null || !VerifyPassword(user.PasswordHash, loginRequest.Password))
      {
        throw new UnauthorizedAccessException("Invalid username or password.");
      }

      var token = jwtTokenGenerator.GenerateToken(user.Username);

      return new LoginResponseDto
      {
        Token = token,
        Expiration = DateTime.UtcNow.AddHours(1)
      };
    }

    private static bool VerifyPassword(string storedHash, string password)
    {
      return BCrypt.Net.BCrypt.Verify(password, storedHash);
    }
    public static string HashPassword(string password)
    {
      return BCrypt.Net.BCrypt.HashPassword(password);
    }
  }
}