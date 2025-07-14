using InventoryService.Infrastructure.Dtos;

namespace InventoryService.Domain.Permission.Dtos
{
    public class PermissionQueryDto : QueryDto
    {
        public string Name { get; set; }
    }
}
