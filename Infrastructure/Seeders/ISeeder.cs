using InventoryService.Infrastructure.Databases;

namespace InventoryService.Infrastructure.Seeders
{
  public interface ISeeder
  {
    Task Seed(DataContext dbContext, ILogger logger);
  }
}
