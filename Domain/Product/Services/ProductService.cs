using InventoryService.Domain.Product.Dtos;
using InventoryService.Domain.Product.Messages;
using InventoryService.Domain.Product.Repositories;
using InventoryService.Infrastructure.Dtos;
using InventoryService.Infrastructure.Exceptions;
using System.Reflection.Metadata.Ecma335;

namespace InventoryService.Domain.Product.Services
{
    public class ProductService(
        ProductQueryRepository productQueryRepository
    )
    {
        private readonly ProductQueryRepository _productQueryRepository = productQueryRepository;

        public async Task<PaginationModel<ProductResultDto>> FindAllAsync(ProductQueryDto query = null)
        {
            var data = await _productQueryRepository.PaginationAsync(query);
            var formatedData = ProductResultDto.MapRepo(data.Data);
            var paginate = PaginationModel<ProductResultDto>.Parse(formatedData, data.Count, query);
            return paginate;
        }

        public async Task<ProductResultDto> FindOneById(Guid id = default)
        {
            var data = await _productQueryRepository.FindOneById(id) ??
                throw new DataNotFoundException(ProductErrorMessage.ErrProductNotFound);

            return new ProductResultDto(data);
        }

        public List<ProductResultDto> FindAll(ProductQueryDto query = null)
        {
            var data = _productQueryRepository.Pagination(query);
            var formatedData = ProductResultDto.MapRepo(data.Data);
            return formatedData;
        }

        public List<ProductResultDto> FindByIds(List<Guid> ids = null)
        {
            var data = _productQueryRepository.FindByIds(ids);
            return ProductResultDto.MapRepo(data);
        }

        public async Task<bool> UpdateStockAsync(List<UpdateStockProductDto> data)
        {
            var result = await _productQueryRepository.UpdateStockAsync(data);
            return result;
        }
    }
}