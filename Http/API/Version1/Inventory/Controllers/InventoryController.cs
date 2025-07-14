using InventoryService.Infrastructure.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryService.Http.API.Version1.Inventory.Controllers
{
    [Route("api/v1/inventory")]
    [ApiController]
    [AllowAnonymous]
    public class InventoryController : ControllerBase
    {
        private readonly ILogger<InventoryController> _logger;

        public InventoryController(ILogger<InventoryController> logger)
        {
            _logger = logger;
        }

        [HttpGet()]
        public async Task<ApiResponse> Test()
        {
            var data = await Task.Run(() =>
            {
                return new ApiResponseData<object>(System.Net.HttpStatusCode.OK, new { message = "This is a test" });
            });

            return data;
        }

    }
}