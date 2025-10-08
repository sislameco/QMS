using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Dto.Pagination;
using Models.Dto.UserManagement;
using Services.UserManagement;

namespace WebApi.Controllers.Auth
{
    [ApiController]
    [Route("role")]
    [AllowAnonymous]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet("all")]
        public async Task<PaginationResponse<RoleWithUsersDto>> GetRoles()
        {
            return await _roleService.GetRolesWithUsersAsync();
        }
        [HttpGet]
        public async Task<IActionResult> GetById(int roleId)
        {
            var data = await _roleService.GetRoleByIdAsync(roleId);
            return Ok(data);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RoleInputDto input)
        {
            await _roleService.CreateRole(input);
            return Ok();
        }
        [HttpPut("{roleId}")]
        public async Task<IActionResult> Update(int roleId,[FromBody] RoleInputDto input)
        {
            await _roleService.UpdateRole(roleId,input);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _roleService.DeleteRole(id);
            return Ok();
        }
        [HttpGet("dropdown")]
        public async Task<IActionResult> GetRoleDropdown()
        {
            var roles = await _roleService.GetRolesWithUsersAsync();
            var dropdown = roles.Items.Select(r => new { r.RoleId, r.RoleName }).ToList();
            return Ok(dropdown);
        }
    }
}