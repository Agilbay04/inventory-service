using InventoryService.Domain.Permission.Repositories;
using InventoryService.Infrastructure.Exceptions;
using InventoryService.Infrastructure.Dtos;
using InventoryService.Domain.Permission.Dtos;
using InventoryService.Domain.Permission.Messages;

namespace InventoryService.Domain.Permission.Services
{
    public class PermissionService(
        PermissionStoreRepository permissionStoreRepository,
        PermissionQueryRepository permissionQueryRepository
    )
    {
        private readonly PermissionStoreRepository _permissionStoreRepository = permissionStoreRepository;
        private readonly PermissionQueryRepository _permissionQueryRepository = permissionQueryRepository;

        public async Task<PaginationModel<PermissionResultDto>> Index(PermissionQueryDto query)
        {
            var result = await _permissionQueryRepository.Pagination(query);
            var formattedResult = PermissionResultDto.MapRepo(result.Data);
            var paginate = PaginationModel<PermissionResultDto>.Parse(formattedResult, result.Count, query);
            return paginate;
        }

        public async Task Create(PermissionCreateDto dataCreate)
        {
            var isPermissionExist = await _permissionQueryRepository.IsExistByKey(dataCreate.Key);

            if (isPermissionExist)
            {
                throw new UnprocessableEntityException(PermissionErrorMessage.ErrPermissionAlreadyExist);
            }

            var data = PermissionCreateDto.Assign(dataCreate);

            await _permissionStoreRepository.Create(data);
        }

        public async Task<PermissionResultDto> DetailById(Guid id)
        {
            var permission = await _permissionQueryRepository.FindOneById(id);

            if (permission == null)
            {
                throw new DataNotFoundException(PermissionErrorMessage.ErrPermissionNotFound);
            }

            return new PermissionResultDto(permission);
        }

        public async Task<List<Models.Permission>> GetList(string search, int page, int perPage)
        {
            return await _permissionQueryRepository.Get(search, page, perPage);
        }
        public async Task<int> Count(string search)
        {
            return await _permissionQueryRepository.CountAll(search);
        }

        public async Task Update(Guid id, PermissionUpdateDto dataUpdate)
        {
            var data = PermissionUpdateDto.Assign(dataUpdate);
            await _permissionStoreRepository.Update(id, data);
        }

        public async Task Delete(Guid id)
        {
            await _permissionStoreRepository.Delete(id);
        }
    }
}