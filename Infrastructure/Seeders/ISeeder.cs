using InventoryService.Infrastructure.Databases;

namespace InventoryService.Infrastructure.Seeders
{
  public interface ISeeder
  {
    Task Seed(DotnetServiceDBContext dbContext, ILogger logger);
  }
}
