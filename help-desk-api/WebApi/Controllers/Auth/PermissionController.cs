using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Dto.Menus;
using Models.Dto.UserManagement;
using Services.UserManagement;
using WebApi.Helper.Security;

namespace WebApi.Controllers.Auth
{
    [ApiController]
    [Route("permission")]
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


        [HttpGet("menu")]
        public async Task<IActionResult> GetMenuAccess(int roleId = 1)
        {

            return Ok(await _permissionService.GetMenuAccess(roleId));
        }

        [HttpPut]
        public async Task<IActionResult> SetMenuPermission(int roleId, List<RoleSetWithMenuActoinDto> FKMenuActionIds)
        {
            return Ok(await _permissionService.SetMenuPermission(roleId, FKMenuActionIds));
        }

        [HttpGet()]
        public async Task<IActionResult> GetRoles(int userId)
        {
            var result = await _permissionService.GetUserRolesAsync(userId);
            return Ok(result);
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> RemoveRoles(int userId, [FromBody] List<int> roleIds)
        {
            await _permissionService.RemoveUserRolesAsync(userId, roleIds);
            return Ok(new { Message = "Roles removed successfully" });
        }

        [HttpGet]
        [Route("get-menus")]
        [ProducesResponseType(typeof(PermittedMenuDto), 200)]
        public async Task<IActionResult> PermittedMenus()
        {
            return Ok(await _permissionService.GetLoginUserMenus());
        }
        [HttpGet]
        [Route("modules")]
        //[ProducesResponseType(typeof(DropdownOutputDto<int, string>), 200)]
        public async Task<IActionResult> GetModules()
        {
               return Ok(await _permissionService.GetModules());
        }

    }

}
