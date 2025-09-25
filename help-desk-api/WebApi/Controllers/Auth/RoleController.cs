using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Dto.UserManagement;
using Services.UserManagement;

namespace WebApi.Controllers.Auth
{
    [ApiController]
    [Route("permission")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet("menu")]
        [AllowAnonymous]
        public async Task<IActionResult> GetMenuAccess(int roleId = 1)
        {
            
            return Ok(await _roleService.GetMenuAccess(roleId));
        }

        [HttpPut]
        [AllowAnonymous]
        public async Task<IActionResult> SetMenuPermission(int roleId, List<RoleSetWithMenuActoinDto> FKMenuActionIds)
        {
            return Ok(await _roleService.SetMenuPermission(roleId, FKMenuActionIds));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RoleInputDto dto)
        {
            await _roleService.CreateRole(dto);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] RoleUpdateInputDto dto)
        {
            await _roleService.UpdateRole(dto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _roleService.DeleteRole(id);
            return Ok();
        }
    }
}