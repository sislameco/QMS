using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Dto.Org;
using Models.Entities.Setup;
using Services.Org;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Controllers.Org
{
    [ApiController]
    [Route("email-configuration")]
    [AllowAnonymous]
    public class EmailConfigurationController : ControllerBase
    {
        private readonly IEmailConfigurationService _emailConfigurationService;

        public EmailConfigurationController(IEmailConfigurationService emailConfigurationService)
        {
            _emailConfigurationService = emailConfigurationService;
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateFields([FromBody] EmailConfigInputDto input)
        {
            var result = await _emailConfigurationService.UpdateFieldsAsync(input);
            if (!result)
                return BadRequest("Update failed.");
            return Ok();
        }

        [HttpPut("set-default/{id}")]
        public async Task<IActionResult> SetDefault(int id)
        {
            var result = await _emailConfigurationService.SetDefaultAsync(id);
            return Ok(result);
        }

        [HttpGet("all/{fkCompanyId}")]
        public async Task<ActionResult<List<EmailConfigurationModel>>> GetAllActiveByCompanyId(int fkCompanyId)
        {
            var result = await _emailConfigurationService.GetAllActiveByCompanyIdAsync(fkCompanyId);
            return Ok(result);
        }
    }
}
