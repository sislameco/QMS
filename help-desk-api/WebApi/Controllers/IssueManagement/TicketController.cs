using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Update.Internal;
using Models.Dto.Ticket;
using Services.IssueManagement;
using WebApi.Helper.Security;

namespace WebApi.Controllers.IssueManagement
{
    [ApiController]
    [Route("ticket")]
    [CustomAuthorization]
    public class TicketController : ControllerBase
    {
        private readonly ITicketReferenceService _ticketReferenceService;
        private readonly ITicketService _ticketService;
        public TicketController(ITicketReferenceService ticketReferenceService, ITicketService ticketService)
        {
            _ticketReferenceService = ticketReferenceService;
            _ticketService = ticketService;
        }
        // generate create endpoint _ticketService.CreateTicket
        [HttpGet("list")]
        public async Task<IActionResult> GetTickets(int fkCompanyId)
        {
            //var tickets = TicketSeed.GetTickets();
            var tickets = await _ticketService.GetTicketLists();
            var response = new
            {
                items = tickets,
                total = tickets.Count,
                page = 1,
                pageSize = tickets.Count
            };

            return Ok(response);
        }


        [HttpPost("create")]
        public async Task<IActionResult> CreateTicket([FromBody] AddTicketInputDto input)
        {
            var result = await _ticketService.CreateTicket(input);
            return Ok(new { ticketId = result });
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> TicketView(int id)
        {
            var result = await _ticketService.TicketView(id);
            return Ok(new { ticketId = result });
        }



        // add/update comment NgxEditorModule
        // add/revove wattcher
        // add/remove
        // remvoe attachment

        // Section wise update of ticket
        //   1. Subject 
        //   Description NgxEditorModule 
        // view ticket details
        // update ticket details
    }
}