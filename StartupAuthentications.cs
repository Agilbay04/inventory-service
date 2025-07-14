using InventoryService.Domain.Auth.Util;
using InventoryService.Infrastructure.Databases;

namespace DotNetService
{
    public partial class Startup
    {
        public void Authentications(IServiceCollection services)
        {
            services.AddSingleton<LocalStorageDatabase>();
            services.AddSingleton<AuthUtil>();
        }
    }
}
