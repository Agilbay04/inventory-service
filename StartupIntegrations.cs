using InventoryService.Infrastructure.BackgroundHosted;
using InventoryService.Infrastructure.Integrations.Http;
using InventoryService.Infrastructure.Integrations.NATs;

namespace InventoryService
{
    public partial class Startup
    {
        public void Integrations(IServiceCollection services)
        {
            services.AddScoped<HttpIntegration>();
            services.AddSingleton<NATsIntegration>();
            services.AddSingleton<NATsTask>();
        }
    }
}
