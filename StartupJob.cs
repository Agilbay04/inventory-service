using InventoryService.Infrastructure.Jobs;

namespace InventoryService
{
    public partial class Startup
    {
        public void Jobs(IServiceCollection services)
        {
            services.AddScoped<NotificationHouseKeepingJob>();
        }
    }
}
