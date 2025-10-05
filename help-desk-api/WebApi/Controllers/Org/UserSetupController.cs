using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Dto.Org;
using Services.Org;
using WebApi.Helper.Security;

namespace WebApi.Controllers.Org
{
    [ApiController]
    [Route("tenant-user")]
    [AllowAnonymous]
    public class UserSetupController : ControllerBase
    {
        private readonly ITenantUserService _tenantUserService;
        public UserSetupController(ITenantUserService tenantUserService)
        {
            _tenantUserService = tenantUserService;
        }
        [HttpGet("all/{companyId}")]
        public async Task<IActionResult> GetAllUsers(int companyId, [FromQuery] UserPaginationInputDto input)
        {
            var departments = await _tenantUserService.GetUsersByTenantAsync(companyId, input);
            return Ok(departments);
        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser([FromBody] UserSetupInputDto dto)
        {
            var result = await _tenantUserService.UpdateUserAsync(dto);
            if (result)
                return Ok();
            return BadRequest("Update failed");
        }
        // generate department get by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserId(int id)
        {
            var department = await _tenantUserService.GetUserByIdAsync(id);
            return Ok(department);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _tenantUserService.DeleteUserAsync(id);
            if (result)
                return Ok();
            return BadRequest("Delete failed");
        }
    }
}
