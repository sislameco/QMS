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