using PWSmartCommerce.Domain.DTOs.Login;

namespace PWSmartCommerce.Services.Interfaces
{
  public interface IAuthService
  {
    Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequest);
  }
}