using InventoryService.Infrastructure.Dtos;

namespace InventoryService.Domain.Product.Dtos
{
    public class ProductQueryDto : QueryDto
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }
}