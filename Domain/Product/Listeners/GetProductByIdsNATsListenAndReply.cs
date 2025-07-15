using InventoryService.Domain.Product.Dtos;
using InventoryService.Domain.Product.Messages;
using InventoryService.Domain.Product.Services;
using InventoryService.Infrastructure.Helpers;
using InventoryService.Infrastructure.Shareds;
using InventoryService.Infrastructure.Subscriptions;

namespace InventoryService.Domain.Product.Listeners
{
    public class GetProductByIdsNATsListenAndReply(
        ILoggerFactory loggerFactory,
        ProductService productService
    ) : IReplyAction<IDictionary<string, object>, IDictionary<string, object>>
    {
        private readonly ILogger<GetProductByIdsNATsListenAndReply> _logger = loggerFactory.CreateLogger<GetProductByIdsNATsListenAndReply>();
        private readonly ProductService _productService = productService;

        public IDictionary<string, object> Reply(IDictionary<string, object> data)
        {
            try
            {
                var jsonData = Utils.JsonSerialize(data);
                _logger.LogInformation("Get Subscribed data: {jsonData}", jsonData);

                var responseData = Utils.JsonDeserialize<ApiResponseData<GetProductByIdsDto>>(jsonData);
                List<Guid> param = responseData.Data.Ids;

                var result = _productService.FindByIds(param);

                if (result == null || result.Count == 0)
                {
                    return Utils.ErrorResponseFormat(ProductErrorMessage.ErrProductNotFound);
                }

                return Utils.SuccessResponseFormat(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "<{className}> Error On Get Product By Ids: {messages}",
                    nameof(GetProductByIdsNATsListenAndReply),
                    ex.Message
                );
                return Utils.ErrorResponseFormat(ex.Message);
            }
        }
        
    }
}