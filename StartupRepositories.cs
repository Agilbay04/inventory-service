using InventoryService.Domain.Auth.Repositories;
using InventoryService.Domain.Notification.Repositories;
using InventoryService.Domain.Permission.Repositories;
using InventoryService.Domain.Role.Repositories;
using InventoryService.Domain.User.Repositories;

namespace DotNetService
{
    public partial class Startup
    {
        public void Repositories(IServiceCollection services)
        {
            services.AddScoped<AuthStoreRepository>();
            services.AddScoped<AuthQueryRepository>();
            services.AddScoped<UserQueryRepository>();
            services.AddScoped<UserStoreRepository>();
            services.AddScoped<RoleQueryRepository>();
            services.AddScoped<RoleStoreRepository>();
            services.AddScoped<PermissionQueryRepository>();
            services.AddScoped<PermissionStoreRepository>();
            services.AddScoped<NotificationQueryRepository>();
            services.AddScoped<NotificationStoreRepository>();
        }
    }
}
