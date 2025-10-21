using Models.Dto.Notification;
using Models.Dto.Pagination;
using Models.Entities.Notification;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Notification
{
    // get data NotificationScheduleModel using unitofwork and filter by NotificationPagination 
    public interface INotificationService
    {
        Task<PaginationResponse<NotificationScheduleModel>> GetSchedulesAsync(NotificationPagination pagination);
        Task<bool> ReadNotification(int notificationId);
    }
    public class NotificationService : INotificationService
    {
        private readonly IUnitOfWork _unitOfWork;
        public NotificationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginationResponse<NotificationScheduleModel>> GetSchedulesAsync(NotificationPagination pagination)
        {
            var repo = _unitOfWork.Repository<NotificationScheduleModel, int>();

            // Build filter predicate based on NotificationType
            Expression<Func<NotificationScheduleModel, bool>> predicate = x =>
                pagination.NotificationType == 0 || x.NotificationType == pagination.NotificationType;

            // Get paged results
            var (items, total) = await repo.GetNotificationPagedAsync(pagination.Page, pagination.PageSize);

            return new()
            {
                Items = items.ToList(),
                Page = pagination.Page,
                PageSize = pagination.PageSize,
                Total = total
            };
        }
        public async Task<bool> ReadNotification(int notificationId)
        {
            var repo = _unitOfWork.Repository<NotificationScheduleModel, int>();
            var notification = await repo.GetByIdAsync(notificationId);
            if (notification == null)
            {
                return false;
            }
            notification.IsRead = true;
            await repo.UpdateAsync(notification);
            await _unitOfWork.CommitAsync();
            return true;
        }
    }
}
