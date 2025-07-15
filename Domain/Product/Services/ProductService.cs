using InventoryService.Domain.Product.Dtos;
using InventoryService.Domain.Product.Messages;
using InventoryService.Domain.Product.Repositories;
using InventoryService.Infrastructure.Dtos;
using InventoryService.Infrastructure.Exceptions;

namespace InventoryService.Domain.Product.Services
{
    public class ProductService(
        ProductQueryRepository productQueryRepository
    )
    {
        private readonly ProductQueryRepository _productQueryRepository = productQueryRepository;

        public async Task<PaginationModel<ProductResultDto>> FindAll(ProductQueryDto query = null)
        {
            var data = await _productQueryRepository.Pagination(query);
            var formatedData = ProductResultDto.MapRepo(data.Data);
            var paginate = PaginationModel<ProductResultDto>.Parse(formatedData, data.Count, query);
            return paginate;
        }

        public async Task<ProductResultDto> FindOneById(Guid id = default)
        {
            var data = await _productQueryRepository.FindOneById(id) ??
                throw new DataNotFoundException(ProductErrorMessagge.ErrProductNotFound);

            return new ProductResultDto(data);
        }
    }
}