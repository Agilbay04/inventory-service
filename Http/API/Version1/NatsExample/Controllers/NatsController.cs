using Microsoft.AspNetCore.Mvc;
using System.Net;
using InventoryService.Infrastructure.Helpers;
using InventoryService.Infrastructure.Integrations.NATs;
using InventoryService.Infrastructure.Shareds;

namespace InventoryService.Http.API.Version1.NatsExample.Controllers
{
    [Route("api/v1/nats")]
    [ApiController]
    public class NatsController(
        NATsIntegration natsIntegration
    ) : ControllerBase
    {
        [HttpPost("{Subject}/{Message}")]
        public async Task<ApiResponse> SendNats(string Subject, string Message)
        {
            var reply = await natsIntegration.PublishAndGetReply<object, object>(
                Subject,
                Utils.JsonSerialize(new {
                    Message
                })
            );

            return new ApiResponseData<object>(HttpStatusCode.OK, reply);
        }
    }
}
