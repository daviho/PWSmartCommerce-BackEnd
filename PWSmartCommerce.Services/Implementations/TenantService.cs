using PWSmartCommerce.DataAccess.Repositories;
using PWSmartCommerce.Domain.DTOs.Tenant;
using PWSmartCommerce.Domain.Models;
using PWSmartCommerce.Services.Interfaces;

namespace PWSmartCommerce.Services.Implementations
{
  public class TenantService(TenantRepository tenantRepository) : ITenantService
  {
    public async Task<IEnumerable<TenantResponseDto>> GetAllTenantsAsync()
    {
      var tenants = await tenantRepository.GetAllAsync();

      return tenants.Select(tenant => new TenantResponseDto
      {
        TenantId = tenant.TenantId,
        Name = tenant.Name,
        TaxId = tenant.TaxId,
        Address = tenant.Address,
        City = tenant.City,
        State = tenant.State,
        Country = tenant.Country,
        CreatedAt = tenant.CreatedAt,
        UpdatedAt = tenant.UpdatedAt
      });
    }

    public async Task<TenantResponseDto?> GetTenantByIdAsync(int id)
    {
      var tenant = await tenantRepository.GetByIdAsync(id);

      if (tenant == null)
        return null;

      return new TenantResponseDto
      {
        TenantId = tenant.TenantId,
        Name = tenant.Name,
        TaxId = tenant.TaxId,
        Address = tenant.Address,
        City = tenant.City,
        State = tenant.State,
        Country = tenant.Country,
        CreatedAt = tenant.CreatedAt,
        UpdatedAt = tenant.UpdatedAt
      };
    }

    public async Task<TenantResponseDto> CreateTenantAsync(TenantCreateDto tenantDto)
    {
      var newTenant = new Tenant
      {
        Name = tenantDto.Name,
        TaxId = tenantDto.TaxId,
        Address = tenantDto.Address,
        City = tenantDto.City,
        State = tenantDto.State,
        Country = tenantDto.Country,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
      };
      var tenantId = await tenantRepository.CreateAsync(newTenant);

      var createdTenant = await tenantRepository.GetByIdAsync(tenantId);

      TenantResponseDto result = new();
      if (createdTenant != null)
      {
        result = new TenantResponseDto()
        {
          TenantId = createdTenant.TenantId,
          Name = createdTenant.Name,
          TaxId = createdTenant.TaxId,
          Address = createdTenant.Address,
          City = createdTenant.City,
          State = createdTenant.State,
          Country = createdTenant.Country,
          CreatedAt = createdTenant.CreatedAt,
          UpdatedAt = createdTenant.UpdatedAt
        };
      }
      return result;
    }

    public async Task<TenantResponseDto?> UpdateTenantAsync(int id, TenantUpdateDto tenantDto)
    {
      var existingTenant = await tenantRepository.GetByIdAsync(id);

      if (existingTenant == null)
        return null;

      existingTenant.Name = tenantDto.Name;
      existingTenant.TaxId = tenantDto.TaxId;
      existingTenant.Address = tenantDto.Address;
      existingTenant.City = tenantDto.City;
      existingTenant.State = tenantDto.State;
      existingTenant.Country = tenantDto.Country;
      existingTenant.UpdatedAt = DateTime.UtcNow;

      _ = await tenantRepository.UpdateAsync(existingTenant);

      var updatedTenant = await tenantRepository.GetByIdAsync(id);

      TenantResponseDto result = new();

      if (updatedTenant != null)
        result = new TenantResponseDto
        {
          TenantId = updatedTenant.TenantId,
          Name = updatedTenant.Name,
          TaxId = updatedTenant.TaxId,
          Address = updatedTenant.Address,
          City = updatedTenant.City,
          State = updatedTenant.State,
          Country = updatedTenant.Country,
          CreatedAt = updatedTenant.CreatedAt,
          UpdatedAt = updatedTenant.UpdatedAt
        };
      return result;
    }

    public async Task<bool> DeleteTenantAsync(int id)
    {
      var existingTenant = await tenantRepository.GetByIdAsync(id);

      if (existingTenant == null)
        return false;

      return await tenantRepository.DeleteAsync(id);
    }
  }
}
