using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.IssueManagement;

namespace WebApi.Controllers.IssueManagement
{
    [ApiController]
    [Route("filter")]
    [AllowAnonymous]
    public class FilterController : ControllerBase
    {
        private readonly ITicketFilterService _filterService;
        public FilterController(ITicketFilterService filterService)
        {
            _filterService = filterService;
        }
    }
}
