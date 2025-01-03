using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace PWSmartCommerce.Services.Utils
{
  public class JwtTokenGenerator(IConfiguration configuration) : IJwtTokenGenerator
  {
    public string GenerateToken(string username)
    {
      var jwtSettings = configuration.GetSection("Jwt");
      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"] ?? string.Empty));
      var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

      var token = new JwtSecurityToken(
        issuer: jwtSettings["Issuer"],
        audience: jwtSettings["Audience"],
        claims:
        [
          new Claim(ClaimTypes.Name, username)
        ],
        expires: DateTime.UtcNow.AddHours(1),
        signingCredentials: credentials);

      return new JwtSecurityTokenHandler().WriteToken(token);
    }
  }
}