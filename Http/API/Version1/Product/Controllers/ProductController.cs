using InventoryService.Domain.Product.Dtos;
using InventoryService.Domain.Product.Services;
using InventoryService.Infrastructure.Dtos;
using InventoryService.Infrastructure.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace InventoryService.Http.API.Version1.Product.Controllers
{
    [Route("api/v1/products")]
    [ApiController]
    [AllowAnonymous]
    public class InventoryController(
        ProductService productService
    ) : ControllerBase
    {
        private readonly ProductService _productService = productService;

        [HttpGet()]
        public async Task<ApiResponse> Index([FromQuery] ProductQueryDto query)
        {
            var data = await _productService.FindAll(query);
            return new ApiResponseData<PaginationModel<ProductResultDto>>(HttpStatusCode.OK, data);
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse> GetOneById(Guid id = default)
        {
            var data = await _productService.FindOneById(id);
            return new ApiResponseData<ProductResultDto>(HttpStatusCode.OK, data);
        }
    }
}