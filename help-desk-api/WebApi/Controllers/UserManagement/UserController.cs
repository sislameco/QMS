using Microsoft.AspNetCore.Mvc;
using Services.Dashboard;

namespace WebApi.Controllers.UserManagement
{
    [ApiController]
    [Route("dashboard")]
    public class UserController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;
        public UserController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }
    }
}
