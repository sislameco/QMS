using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Dto.GlobalDto;
using Services.IssueManagement;

namespace WebApi.Controllers.IssueManagement
{
    [ApiController]
    [Route("ticket-reference")]
    public class TicketReferenceController : ControllerBase
    {
        private readonly ITicketReferenceService _ticketReferenceService;
        public TicketReferenceController(ITicketReferenceService ticketReferenceService)
        {
            _ticketReferenceService = ticketReferenceService;
        }

        [AllowAnonymous]
        [HttpGet("department")]
        public IActionResult GetDepartments(int fkCompanyId=1)
        {
            var departments = _ticketReferenceService.GetDepartments(fkCompanyId);
            return Ok(departments);
        }

        [AllowAnonymous]
        [HttpGet("ticket-type")]
        public IActionResult GetTicketTypes(int fkCompanyId = 1)
        {
            var ticketTypes = _ticketReferenceService.GetTicketTypes(fkCompanyId);
            return Ok(ticketTypes);
        }

        [AllowAnonymous]
        [HttpGet("root-cause")]
        public IActionResult GetRootCauses(int fkCompanyId = 1)
        {
            var rootCauses = _ticketReferenceService.GetRootCauses(fkCompanyId);
            return Ok(rootCauses);
        }

        [AllowAnonymous]
        [HttpGet("resolution")]
        public IActionResult GetResolutions(int fkCompanyId = 1)
        {
            var relocations = _ticketReferenceService.GetRelocations(fkCompanyId);
            return Ok(relocations);
        }

        [AllowAnonymous]
        [HttpGet("customer")]
        public IActionResult GetCustomers(int fkCompanyId = 1)
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
        public IActionResult GetUsers(int fkCompanyId = 1)
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

        [AllowAnonymous]
        [HttpGet("tickets/{fkCompanyId}")]
        public IActionResult GetTickets(int fkCompanyId)
        {
            var tickets = _ticketReferenceService.GetTickets(fkCompanyId);
            return Ok(tickets);
        }

    }
}