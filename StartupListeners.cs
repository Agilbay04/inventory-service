using InventoryService.Domain.Logging.Listeners;

namespace InventoryService
{
    public partial class Startup
    {
        public void Listeners(IServiceCollection services)
        {
            services.AddScoped<LoggingNATsListener>();
            services.AddScoped<LoggingNATsListenAndReply>();
        }
    }
}
