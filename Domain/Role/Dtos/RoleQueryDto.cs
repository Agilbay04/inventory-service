using InventoryService.Infrastructure.Dtos;

namespace InventoryService.Domain.Role.Dtos
{
    public class RoleQueryDto : QueryDto
    {
        public string Name { get; set; }
    }
}