

using InventoryService.Infrastructure.Dtos;

namespace InventoryService.Domain.User.Dtos
{
    public class UserQueryDto : QueryDto
    {
        public string Email { get; set; }
    }
}