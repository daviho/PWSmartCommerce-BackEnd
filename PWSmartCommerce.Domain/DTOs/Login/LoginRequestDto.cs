using System.ComponentModel.DataAnnotations;

namespace PWSmartCommerce.Domain.DTOs.Login
{
  public class LoginRequestDto
  {
    [Required]
    public string UserName { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;
  }
}