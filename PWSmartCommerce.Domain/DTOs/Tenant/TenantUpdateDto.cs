using System.ComponentModel.DataAnnotations;
namespace PWSmartCommerce.Domain.DTOs.Tenant
{
  public class TenantUpdateDto : TenantCreateDto
  {
    [Required] public int TenantId { get; set; }
  }
}