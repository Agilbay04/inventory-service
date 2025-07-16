using InventoryService.Domain.Product.Dtos;
using InventoryService.Domain.Product.Messages;
using InventoryService.Domain.Product.Services;
using InventoryService.Infrastructure.Helpers;
using InventoryService.Infrastructure.Shareds;
using InventoryService.Infrastructure.Subscriptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryService.Domain.Product.Listeners
{
    public class UpdateStockProductNATsListener(
        ILoggerFactory loggerFactory,
        ProductService productService
    ) : IReplyAction<IDictionary<string, object>, IDictionary<string, object>>
    {
        private readonly ILogger<UpdateStockProductNATsListener> _logger = loggerFactory.CreateLogger<UpdateStockProductNATsListener>();
        private readonly ProductService _productService = productService;

        public IDictionary<string, object> Reply(IDictionary<string, object> data)
        {
            try
            {
                var jsonData = Utils.JsonSerialize(data);
                _logger.LogInformation("Get Subscribed data: {jsonData}", jsonData);

                var responseData = Utils.JsonDeserialize<ApiResponseData<List<UpdateStockProductDto>>>(jsonData);
                List<UpdateStockProductDto> param = responseData.Data;

                var result = _productService.UpdateStockAsync(param);

                if (result == null || result.Result == false)
                {
                    return Utils.ErrorResponseFormat(ProductErrorMessage.ErrUpdateStockProduct);
                }

                return Utils.SuccessResponseFormat(result.Result);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "<{className}> Error On Update Stock Product: {messages}",
                    nameof(UpdateStockProductNATsListener),
                    ex.Message
                );
                return Utils.ErrorResponseFormat(ex.Message);
            }
        }
    }
}