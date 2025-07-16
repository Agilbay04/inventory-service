using InventoryService.Infrastructure.Databases;

namespace InventoryService
{
    public partial class Startup
    {
        public void Authentications(IServiceCollection services)
        {
            services.AddSingleton<LocalStorageDatabase>();
        }
    }
}
