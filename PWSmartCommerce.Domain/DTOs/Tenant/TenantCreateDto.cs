using System.ComponentModel.DataAnnotations;
namespace PWSmartCommerce.Domain.DTOs.Tenant
{
  public class TenantCreateDto
  {
    [Required] 
    public string Name { get; set; } = string.Empty;
    public string TaxId { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
  }
}