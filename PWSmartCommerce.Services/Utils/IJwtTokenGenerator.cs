namespace PWSmartCommerce.Services.Utils
{
  public interface IJwtTokenGenerator
  {
    string GenerateToken(string username);
  }
}