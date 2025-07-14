using InventoryService.Commands;
using RuangDeveloper.AspNetCore.Command;

namespace InventoryService
{
    public partial class Startup
    {
        public void Commands(IServiceCollection services)
        {
            services.AddCommands(configure => {
                configure.AddCommand<SeederCommand>();
            });
        }
    }
}
