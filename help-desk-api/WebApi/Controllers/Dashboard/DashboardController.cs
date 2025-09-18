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
        [HttpGet("ticket")]
        public async Task<ActionResult<TicketOutPutDto>> GetTicketSummary()
        {
            var result = await _dashboardService.GetTicketSummary();
            return Ok(result);
        }
    }
}