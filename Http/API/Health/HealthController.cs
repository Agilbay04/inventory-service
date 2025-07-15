using InventoryService.Infrastructure.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryService.Http.API.Health
{
    [Route("inventory-service/health")]
    [ApiController]
    [AllowAnonymous]
    public class HealthController
    {
        [HttpGet]
        public ApiResponseData<object> Get()
        {
            return new ApiResponseData<object>(
                System.Net.HttpStatusCode.OK,
                new { message = "Inventory Service is running, and Healty" }
            );
        }
    }
}
