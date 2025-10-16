using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Dto.Dashboard;
using Models.Dto.Ticket.Models.Dto.Tickets;
using Services.IssueManagement; // Add this using

namespace WebApi.Controllers.IssueManagement
{
    [ApiController]
    [Route("ticket")]
    public class TicketController : ControllerBase
    {
        private readonly ITicketReferenceService _ticketReferenceService;

        public TicketController(ITicketReferenceService ticketReferenceService)
        {
            _ticketReferenceService = ticketReferenceService;
        }

        [AllowAnonymous]
        [HttpGet("list")]
        public IActionResult GetTickets(int fkCompanyId)
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

        #region DDl For Ticket

        [AllowAnonymous]
        [HttpGet("department")]
        public IActionResult GetDepartments(int fkCompanyId)
        {
            var departments = _ticketReferenceService.GetDepartments(fkCompanyId);
            return Ok(departments);
        }

        [AllowAnonymous]
        [HttpGet("ticket-type")]
        public IActionResult GetTicketTypes(int fkCompanyId)
        {
            var ticketTypes = _ticketReferenceService.GetTicketTypes(fkCompanyId);
            return Ok(ticketTypes);
        }

        [AllowAnonymous]
        [HttpGet("root-cause")]
        public IActionResult GetRootCauses(int fkCompanyId)
        {
            var rootCauses = _ticketReferenceService.GetRootCauses(fkCompanyId);
            return Ok(rootCauses);
        }

        [AllowAnonymous]
        [HttpGet("relocation")]
        public IActionResult GetRelocations(int fkCompanyId)
        {
            var relocations = _ticketReferenceService.GetRelocations(fkCompanyId);
            return Ok(relocations);
        }

        [AllowAnonymous]
        [HttpGet("customer")]
        public IActionResult GetCustomers(int fkCompanyId)
        {
            var customers = _ticketReferenceService.GetCustomers(fkCompanyId);
            return Ok(customers);
        }

        [AllowAnonymous]
        [HttpGet("project")]
        public IActionResult GetProjects(int fkCompanyId)
        {
            var projects = _ticketReferenceService.GetProjects(fkCompanyId);
            return Ok(projects);
        }

        [AllowAnonymous]
        [HttpGet("user")]
        public IActionResult GetUsers(int fkCompanyId)
        {
            var users = _ticketReferenceService.GetUsers(fkCompanyId);
            return Ok(users);
        }

        [AllowAnonymous]
        [HttpGet("subform/{ticketTypeId}")]
        public IActionResult GetSubforms(int ticketTypeId)
        {
            var subforms = _ticketReferenceService.GetSubforms(ticketTypeId);
            return Ok(subforms);
        }

        #endregion
    }
}