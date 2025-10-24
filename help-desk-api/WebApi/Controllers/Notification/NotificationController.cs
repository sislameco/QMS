using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Dto.GlobalDto;
using Models.Dto.Notification;
using Models.Entities.Notification;
using Org.BouncyCastle.Pqc.Crypto.Lms;
using Services.Notification;
using Services.Org;
using Swashbuckle.AspNetCore.Annotations;


namespace WebApi.Controllers.Org
{
    [ApiController]
    [Route("notification")]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }
        [HttpGet]
        [SwaggerOperation(Description = $"Get paginated notification by event Email = 1,SMS = 2, App = 3, System = 4", Summary = "get notification by signal r")]
        public async Task<PaginatedResponse<NotificationScheduleModel>> GetNotifications([FromQuery] NotificationPagination input)
        {
            var result = await _notificationService.GetSchedulesAsync(input);
            return new PaginatedResponse<NotificationScheduleModel>
            {
                Data  = result.Items,
                Count = result.Total,
                Message = result.Total> 0?  "Notifications fetched successfully": "No notifications found",
            };
        }

        [HttpPost("read/{notificationId}")]
        public async Task<ObjectResponse<bool>> ReadNotification(int notificationId)
        {
            var isRead = await _notificationService.ReadNotification(notificationId);
            return new ObjectResponse<bool>
            {
                Data = isRead,
                Message = isRead ? "Notification marked as read" : "Failed to mark notification as read"
            };
        }
    }
}
