using Microsoft.AspNetCore.Mvc;
using PWSmartCommerce.Domain.DTOs.Login;
using PWSmartCommerce.Services.Interfaces;

namespace PWSmartCommerce.API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class AuthController(IAuthService authService) : ControllerBase
  {
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      try
      {
        var response = await authService.LoginAsync(loginRequest);
        return Ok(response);
      }
      catch (UnauthorizedAccessException ex)
      {
        return Unauthorized(new { ex.Message });
      }
      catch (Exception ex)
      {
        return StatusCode(500, new { Message = "An unexpected error occurred.", Details = ex.Message });
      }
    }
  }
}