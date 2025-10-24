using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Dto.Org;
using Models.Entities.Notification;
using Models.Enum;
using Services.Org;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Controllers.Org
{
    [ApiController]
    [Route("notification-configuration")]
    public class NotificationConfigurationController : ControllerBase
    {
        private readonly INotificationTemplateService _notificationService;

        public NotificationConfigurationController(INotificationTemplateService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpPut("update-template/{id}")]
        public async Task<IActionResult> UpdateTemplate([FromBody] NotificationInputDto input)
        {
            var result = await _notificationService.UpdateTemplateAsync(input);
            return Ok(result);
        }

        [HttpPut("update-enabled/{id}")]
        public async Task<IActionResult> UpdateIsEnabled(int id, bool isEnabled)
        {
            var result = await _notificationService.UpdateIsEnabledAsync(id, isEnabled);
            return Ok();
        }

        [HttpGet("all/{fkCompanyId}")]
        public async Task<ActionResult<List<NotificationOutputDto>>> GetAllActiveByCompanyId(int fkCompanyId, EnumNotificationType type)
        {
            var result = await _notificationService.GetAllActiveByCompanyIdAsync(fkCompanyId, type);
            return Ok(result);
        }
    }
}
