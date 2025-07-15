using InventoryService.Domain.Logging.Listeners;
using InventoryService.Domain.Product.Listeners;

namespace InventoryService
{
    public partial class Startup
    {
        public void Listeners(IServiceCollection services)
        {
            services.AddScoped<LoggingNATsListener>();
            services.AddScoped<LoggingNATsListenAndReply>();
            services.AddScoped<GetProductByIdsNATsListenAndReply>();
            services.AddScoped<GetAllProductNATsListener>();
        }
    }
}
