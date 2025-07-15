using InventoryService.Domain.Auth.Services;
using InventoryService.Domain.File.Services;
using InventoryService.Domain.Product.Services;
using InventoryService.Domain.Logging.Services;
using InventoryService.Domain.Notification.Services;
using InventoryService.Domain.Permission.Services;
using InventoryService.Domain.Role.Services;
using InventoryService.Domain.User.Services;
using InventoryService.Infrastructure.Shareds;

namespace InventoryService
{
    public partial class Startup
    {
        public void Services(IServiceCollection services)
        {
            services.AddScoped<AuthService>();
            services.AddScoped<UserService>();
            services.AddScoped<PermissionService>();
            services.AddScoped<RoleService>();

            services.AddScoped<LoggingService>();
            services.AddScoped<StorageService>();
            services.AddScoped<FileService>();

            services.AddScoped<NotificationService>();
            services.AddScoped<ProductService>();
        }
    }
}
