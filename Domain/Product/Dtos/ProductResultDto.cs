namespace InventoryService.Domain.Product.Dtos
{
    public class ProductResultDto(Models.Product product)
    {
        public Guid Id { get; set; } = product.Id;
        public string Code { get; set; } = product.Code;
        public string Name { get; set; } = product.Name;
        public string CategoryName { get; set; } = product.Category.Name;
        public string IsPublish { get; set; } = GetPublishStatus(product.IsPublish);
        public int Stock { get; set; } = product.Stock;
        public decimal Price { get; set; } = product.Price;
        public string CreatedAt { get; set; } = product.CreatedAt?.ToString("yyyy-MM-dd HH:mm:ss");

        private static string GetPublishStatus(bool isPublish)
        {
            return isPublish ? "Published" : "Unpublished";
        }

        public static List<ProductResultDto> MapRepo(List<Models.Product> data)
        {
            return data?.Select(data => new ProductResultDto(data)).ToList();
        }
    }
}