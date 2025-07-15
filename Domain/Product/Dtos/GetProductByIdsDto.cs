namespace InventoryService.Domain.Product.Dtos
{
    public class GetProductByIdsDto
    {
        public List<Guid> Ids { get; set; }
    }
}