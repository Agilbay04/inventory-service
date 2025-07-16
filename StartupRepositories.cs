using InventoryService.Domain.Product.Repositories;

namespace InventoryService
{
    public partial class Startup
    {
        public void Repositories(IServiceCollection services)
        {
            services.AddScoped<ProductQueryRepository>();
        }
    }
}
