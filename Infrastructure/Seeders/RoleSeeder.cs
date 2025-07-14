using System.Text.Json;
using InventoryService.Infrastructure.Databases;
using InventoryService.Infrastructure.Helpers;
using InventoryService.Models;

namespace InventoryService.Infrastructure.Seeders
{
    public class RoleSeeder : ISeeder
    {
        public async Task Seed(DotnetServiceDBContext dbContext, ILogger logger)
        {
            logger.LogInformation("Seeding Roles...");
            var jsonPath = "SeedersData/Role.json";

            using var stream = File.OpenRead(jsonPath);
            var roles = await JsonSerializer.DeserializeAsync<List<Role>>(stream, JsonSerializeSeeder.options);
            var newRoles = new List<Role>();

            try
            {
                await dbContext.Database.BeginTransactionAsync();

                foreach (var role in roles)
                {
                    newRoles.Add(role);
                }
                await dbContext.Roles.AddRangeAsync(newRoles);
                await dbContext.SaveChangesAsync();
                await dbContext.Database.CommitTransactionAsync();
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error while seeding Roles");
                await dbContext.Database.RollbackTransactionAsync();
            }

            logger.LogInformation("Seeding Role Roles complete");
        }
    }
}