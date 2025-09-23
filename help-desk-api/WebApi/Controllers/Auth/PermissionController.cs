using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Dto.Menus;
using Models.Dto.UserManagement;
using Services.UserManagement;
using WebApi.Helper.Security;

namespace WebApi.Controllers.Auth
{
    [ApiController]
    [Route("api/[controller]")]
    [CustomAuthorization]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionService _permissionService;

        public PermissionController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [HttpPost("assign")]
        public async Task<IActionResult> AssignRoles([FromBody] UserRoleAssignDto request)
        {
            var result = await _permissionService.AssignRolesAsync(request);
            return Ok(result);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetRoles(long userId)
        {
            var result = await _permissionService.GetUserRolesAsync(userId);
            return Ok(result);
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> RemoveRoles(long userId, [FromBody] List<long> roleIds)
        {
            await _permissionService.RemoveUserRolesAsync(userId, roleIds);
            return Ok(new { Message = "Roles removed successfully" });
        }

        [HttpGet]
        [Route("get-menus-user-info")]
        [ProducesResponseType(typeof(PermittedMenuDto), 200)]
        public async Task<IActionResult> PermittedMenus()
        {
            return Ok(await _permissionService.GetUserMenus());
        }

    }

}
