using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Org;
using Models.Entities.Notification;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace WebApi.Controllers.Org
{
    [ApiController]
    [Route("notification-configuration")]
    [AllowAnonymous]
    public class NotificationConfigurationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationConfigurationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        //[HttpPut("update-template/{id}")]
        //public async Task<IActionResult> UpdateTemplate(int id, [FromBody] UpdateTemplateDto dto)
        //{
        //    var result = await _notificationService.UpdateTemplateAsync(id, dto.EmailConfigurationId, dto.SubjectTemplate, dto.BodyTemplate, dto.CcList);
        //    if (!result)
        //        return BadRequest("Update failed.");
        //    return Ok();
        //}

        [HttpPut("update-enabled/{id}")]
        public async Task<IActionResult> UpdateIsEnabled(int id, bool isEnabled)
        {
            var result = await _notificationService.UpdateIsEnabledAsync(id, isEnabled);
            if (!result)
                return BadRequest("Update failed.");
            return Ok();
        }

        [HttpGet("all/{fkCompanyId}")]
        public async Task<ActionResult<List<NotificationTemplateModel>>> GetAllActiveByCompanyId(int fkCompanyId)
        {
            var result = await _notificationService.GetAllActiveByCompanyIdAsync(fkCompanyId);
            return Ok(result);
        }
    }
}
