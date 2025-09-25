using Microsoft.AspNetCore.Mvc;
using Models.Dto.UserManagement;
using Services.Dashboard;
using Services.UserManagement;

namespace WebApi.Controllers.UserManagement
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }
    }
}
