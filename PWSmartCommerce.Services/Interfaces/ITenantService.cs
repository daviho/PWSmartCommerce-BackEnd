using PWSmartCommerce.Domain.DTOs.Tenant;
namespace PWSmartCommerce.Services.Interfaces
{
  public interface ITenantService
  {
    Task<IEnumerable<TenantResponseDto>> GetAllTenantsAsync();
    Task<TenantResponseDto?> GetTenantByIdAsync(int id);
    Task<TenantResponseDto> CreateTenantAsync(TenantCreateDto tenantDto);
    Task<TenantResponseDto?> UpdateTenantAsync(int id, TenantUpdateDto tenantDto);
    Task<bool> DeleteTenantAsync(int id);
  }
}