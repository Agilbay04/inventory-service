using InventoryService.Infrastructure.Exceptions;
using InventoryService.Infrastructure.Dtos;
using InventoryService.Domain.Notification.Repositories;
using InventoryService.Domain.Notification.Dtos;
using InventoryService.Domain.Notification.Messages;
using InventoryService.Infrastructure.Shareds;

namespace InventoryService.Domain.Notification.Services
{
    public class NotificationService(
        NotificationQueryRepository NotificationQueryRepository,
        NotificationStoreRepository notificationStoreRepository,
        IHttpContextAccessor httpContextAccessor
    )
    {
        private readonly NotificationQueryRepository _notificationQueryRepository = NotificationQueryRepository;

        private readonly NotificationStoreRepository _notificationStoreRepository = notificationStoreRepository;

        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public async Task<PaginationModel<NotificationResultDto>> Index(NotificationQueryDto query)
        {
            var userId = Utils.GetUserLoggedId(_httpContextAccessor);
            var result = await _notificationQueryRepository.Pagination(query, userId);
            var formattedResult = NotificationResultDto.MapRepo(result.Data);
            var paginate = PaginationModel<NotificationResultDto>.Parse(formattedResult, result.Count, query);
            return paginate;
        }

        public async Task<NotificationResultDto> DetailById(Guid id, Guid userId)
        {
            var notification = await _notificationQueryRepository.FindOneByIdAndUserId(id, userId);

            if (notification == null)
            {
                throw new DataNotFoundException(NotificationErrorMessage.ErrNotificationNotFound);
            }

            return new NotificationResultDto(notification);
        }

        public async Task<bool> UserHasUnreadNotification()
        {
            var userId = Utils.GetUserLoggedId(_httpContextAccessor);
            return await _notificationQueryRepository.HasUnreadNotificationByUserId(userId);
        }

        public async Task ReadNotificationById(Guid id)
        {
            var userId = Utils.GetUserLoggedId(_httpContextAccessor);
            await _notificationStoreRepository.ReadNotificationById(id, userId);
        }

        public async Task ReadAllNotification()
        {
            var userId = Utils.GetUserLoggedId(_httpContextAccessor);
            await _notificationStoreRepository.ReadAllNotificationByUserId(userId);
        }
    }
}