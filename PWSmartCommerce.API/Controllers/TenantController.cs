using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PWSmartCommerce.Domain.DTOs.Tenant;
using PWSmartCommerce.Services.Interfaces;

namespace PWSmartCommerce.API.Controllers
{
  [Authorize]
  [ApiController]
  [Route("api/[controller]")]
  public class TenantController(ITenantService tenantService) : ControllerBase
  {
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      try
      {
        var tenants = await tenantService.GetAllTenantsAsync();
        return Ok(tenants);
      }
      catch (Exception ex)
      {
        return StatusCode(500, new { Message = "An unexpected error occurred.", Details = ex.Message });
      }
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
      try
      {
        var tenant = await tenantService.GetTenantByIdAsync(id);
        if (tenant == null)
        {
          return NotFound(new { Message = "Tenant not found." });
        }

        return Ok(tenant);
      }
      catch (Exception ex)
      {
        return StatusCode(500, new { Message = "An unexpected error occurred.", Details = ex.Message });
      }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TenantCreateDto tenantDto)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      try
      {
        var createdTenant = await tenantService.CreateTenantAsync(tenantDto);
        return CreatedAtAction(nameof(GetById), new { id = createdTenant.TenantId }, createdTenant);
      }
      catch (Exception ex)
      {
        return StatusCode(500, new { Message = "An unexpected error occurred.", Details = ex.Message });
      }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] TenantUpdateDto tenantDto)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      try
      {
        var updatedTenant = await tenantService.UpdateTenantAsync(id, tenantDto);
        if (updatedTenant == null)
        {
          return NotFound(new { Message = "Tenant not found." });
        }

        return Ok(updatedTenant);
      }
      catch (Exception ex)
      {
        return StatusCode(500, new { Message = "An unexpected error occurred.", Details = ex.Message });
      }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
      try
      {
        var deleted = await tenantService.DeleteTenantAsync(id);
        if (!deleted)
        {
          return NotFound(new { Message = "Tenant not found." });
        }

        return NoContent();
      }
      catch (Exception ex)
      {
        return StatusCode(500, new { Message = "An unexpected error occurred.", Details = ex.Message });
      }
    }

  }
}
