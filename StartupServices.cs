using InventoryService.Domain.File.Services;
using InventoryService.Domain.Product.Services;
using InventoryService.Domain.Logging.Services;
using InventoryService.Infrastructure.Shareds;

namespace InventoryService
{
    public partial class Startup
    {
        public void Services(IServiceCollection services)
        {
            services.AddScoped<LoggingService>();
            services.AddScoped<StorageService>();
            services.AddScoped<FileService>();

            services.AddScoped<ProductService>();
        }
    }
}
