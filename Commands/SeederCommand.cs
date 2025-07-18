
using InventoryService.Infrastructure.Databases;
using InventoryService.Infrastructure.Exceptions;
using InventoryService.Infrastructure.Seeders;
using RuangDeveloper.AspNetCore.Command;

namespace InventoryService.Commands
{
    public class SeederCommand(
        IServiceProvider serviceProvider
    ) : ICommand
    {
        private readonly IServiceProvider _serviceProvider = serviceProvider;

        public string Name => "seed";

        public string Description => "Seed the database with initial data";

        public void Execute(string[] args)
        {
        }

        public async Task ExecuteAsync(string[] args)
        {
            var fileNames = args.ToList();
            using var scope = _serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<SeederCommand>>();

            Console.WriteLine("-------------------------- Seed Started -------------------------");

            if (fileNames.Count > 0)
            {
                for (var i = 0; i < fileNames.Count; i++)
                {
                    /* -------------------------- Insert seed data here ------------------------- */
                    var type = Type.GetType("InventoryService.Infrastructure.Seeders." + fileNames[i]);
                    logger.LogInformation("Seeding: {SeederName}", fileNames[i]);
                    if (type != null)
                    {
                        if (Activator.CreateInstance(type) is ISeeder seederType)
                        {
                            await seederType.Seed(dbContext, logger);
                        }
                    }
                    else
                    {
                        throw new DataNotFoundException($"seeder of {fileNames[i]} not found");
                    }
                }
            }
            // When it doesnt have any args, seed all data accordingly
            else
            {
                /* -------------------------- Insert seed data here ------------------------- */
                /* ----------------------- Be careful about the sequences ------------------- */
                await new CategorySeeder().Seed(dbContext, logger);
                await new ProductSeeder().Seed(dbContext, logger);
            }

            logger.LogInformation("-------------------------- Seed Finish --------------------------");
        }
    }
}
