using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Dto.Dashboard;
using Services.Dashboard;

namespace WebApi.Controllers.Dashboard
{
    [ApiController]
    [Route("dashboard")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;
        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }
        [HttpGet("user-data")]
        [AllowAnonymous]
        public async Task<ActionResult<DashboardResponseDto>> GetDashboard()
        {
            var result = await _dashboardService.GetDashboard();
            return Ok(result);
        }
    }
}