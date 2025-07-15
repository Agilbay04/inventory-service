using InventoryService.Infrastructure.Databases;
using InventoryService.Infrastructure.Helpers;
using InventoryService.Models;
using System.Text.Json;

namespace InventoryService.Infrastructure.Seeders
{
    public class CategorySeeder : ISeeder
    {
        public async Task Seed(DataContext dbContext, ILogger logger)
        {
            logger.LogInformation("Seeding Categories...");
            var jsonPath = "SeedersData/Category.json";

            using var stream = File.OpenRead(jsonPath);
            var categories = JsonSerializer.Deserialize<List<Category>>(stream, JsonSerializeSeeder.options);
            var newCategories = new List<Category>();

            try
            {
                await dbContext.Database.BeginTransactionAsync();

                foreach (var category in categories)
                {
                    newCategories.Add(category);
                }
                await dbContext.Categories.AddRangeAsync(newCategories);
                await dbContext.SaveChangesAsync();
                await dbContext.Database.CommitTransactionAsync();
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error while seeding Categories");
                await dbContext.Database.RollbackTransactionAsync();
            }

            logger.LogInformation("Seeding Categories complete");
        }
        
    }
}