using InventoryService.Domain.Inventory.Dtos;
using InventoryService.Infrastructure.Databases;
using InventoryService.Infrastructure.Helpers;
using InventoryService.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace InventoryService.Infrastructure.Seeders
{
    public class ProductSeeder : ISeeder
    {
        public async Task Seed(DataContext dbContext, ILogger logger)
        {
            logger.LogInformation("Seeding Products...");
            var jsonPath = "SeedersData/Product.json";

            using var stream = File.OpenRead(jsonPath);
            var products = JsonSerializer.Deserialize<List<ProductSeederDto>>(stream, JsonSerializeSeeder.options);
            var newProducts = new List<Product>();

            if (products == null || products.Count == 0)
            {
                logger.LogInformation("No products to seed.");
                return;
            }

            try
            {
                await dbContext.Database.BeginTransactionAsync();

                foreach (var product in products)
                {
                    var category = await dbContext.Categories.FirstOrDefaultAsync(data => data.Key == product.Key);
                    if (category == null)
                    {
                        logger.LogWarning("Category not found for Key: {Key}", product.Key);
                        continue;
                    }

                    newProducts.Add(new Product
                    {
                        Name = product.Name,
                        IsPublish = product.IsPublish,
                        Code = product.Code,
                        CategoryId = category.Id,
                        Price = product.Price,
                        Stock = product.Stock
                    });
                }

                await dbContext.Products.AddRangeAsync(newProducts);
                await dbContext.SaveChangesAsync();
                await dbContext.Database.CommitTransactionAsync();
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error while seeding Products");
                await dbContext.Database.RollbackTransactionAsync();
            }

            logger.LogInformation("Seeding Products complete");
        }
    }
}