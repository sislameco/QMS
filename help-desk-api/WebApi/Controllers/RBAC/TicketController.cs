using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Dto.Dashboard;
using Models.Dto.Ticket.Models.Dto.Tickets;

namespace WebApi.Controllers.Dashboard
{
    [ApiController]
    [Route("ticket")]
    public class TicketController : ControllerBase
    {
        [AllowAnonymous]
        [HttpGet("list")]
        public IActionResult GetTickets()
        {
            var tickets = TicketSeed.GetTickets();

            var response = new
            {
                items = tickets,
                total = tickets.Count,
                page = 1,
                pageSize = tickets.Count
            };

            return Ok(response);
        }
    }
}