using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Dto.GlobalDto;
using Models.Dto.Org;
using Models.Dto.Ticket;
using Models.Enum;
using Services.IssueManagement;
using WebApi.Helper.Security;

namespace WebApi.Controllers.IssueManagement
{
    [ApiController]
    [Route("ticket")]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;
        private readonly ITicketReferenceService _ticketReferenceService;
        public TicketController(ITicketService ticketService, ITicketReferenceService ticketReferenceService)
        {
            _ticketService = ticketService;
            _ticketReferenceService = ticketReferenceService;
        }
        [HttpGet("list")]
        public async Task<IActionResult> GetTickets(int companyId, [FromQuery] TicketFilterInputDto input)
        {
            var tickets = await _ticketService.GetTicketLists(companyId, input);
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
        #region Ticket Full Page and Project/Customer Details 1
        [HttpGet("basic-detail/{id}")]
        public async Task<IActionResult> TicketBasicDetails(int id)
        {
            return Ok(await _ticketService.GetBasicDetails(id));
        }
        [HttpPut("basic-detail/{id}")]
        public async Task<IActionResult> UpdateBasicDetails(int id, TicketBasicDetailInputDto input)
        {
            var result = await _ticketService.UpdateBasicDetails(id, input);
            return Ok(new { ticketId = result });
        }

        [HttpGet("specification/{id}")]
        public async Task<IActionResult> TicketSpecification(int id)
        {
            return Ok(await _ticketService.GetSpecification(id));
        }

        [HttpGet("attachment/{id}")]
        public async Task<IActionResult> GetAttachments(int id)
        {
            return Ok(await _ticketService.GetAttachments(id));
        }
        [HttpPost("attachment/{id}")]
        public async Task<IActionResult> AddTicketAttachments(int id, List<int> files)
        {
            return Ok(await _ticketService.AddTicketAttachments(id, files));
        }
        [HttpDelete("attachment")]
        public async Task<IActionResult> DeleteAttachment(int id)
        {
            return Ok(await _ticketService.DeleteAttachment(id));
        }

        [HttpGet("linking-tickets/{id}")]
        public async Task<IActionResult> GetLinkingItems(int id)
        {
            return Ok(await _ticketService.GetLinkingItems(id));
        }
        // for linking
        [HttpGet("get-tickets/{companyId}")]
        public async Task<IActionResult> GetTickets(int companyId)
        {
            return Ok(await _ticketReferenceService.GetTickets(companyId));
        }

        [HttpPost("linking-tickets/{id}")]
        public async Task<IActionResult> AddLinking(int id, List<int> tickets)
        {
            return Ok(await _ticketService.AddLinking(id, tickets));
        }

        [HttpDelete("linking-ticket/{id}")]
        public async Task<IActionResult> DeleteLinking(int id)
        {
            return Ok(await _ticketService.DeleteAttachment(id));
        }


        [HttpGet("get-define-field/{id}")]
        public async Task<IActionResult> GetDefineField(int id)
        {
            return Ok(await _ticketService.GetDefineFields(id));
        }

        [HttpPut("define-field/{id}")]
        public async Task<IActionResult> UpdateDefineData(int id, List<UpdateSubFromInputDto> input)
        {
            return Ok(await _ticketService.UpdateDefineData(id, input));
        }

        [HttpGet("get-watchers/{id}")]
        public async Task<IActionResult> GetWatchers(int id)
        {
            return Ok(await _ticketService.GetWatchers(id));
        }


        [HttpPost("watcher/{ticketId}")]
        public async Task<IActionResult> AddWatcher(int id,int userId)
        {
            return Ok(await _ticketService.AddWatcher(id, userId));
        }

        [HttpDelete("watcher/{Id}")]
        public async Task<IActionResult> DeleteWatcher(int id)
        {
            return Ok(await _ticketService.DeleteWatcher(id));
        }

        #endregion

        #region Ticket Full Page and Project/Customer Details 1
        [HttpPut("specification/{id}")]
        public async Task<IActionResult> UpdateSpecification(int id, TicketSpecificationOutputDto input)
        {
            var result = await _ticketService.UpdateSpecification(id, input);
            return Ok(new { ticketId = result });
        }

        [HttpPut("change-status/{id}")]
        public async Task<IActionResult> ChangeTicketStatus(int id, EnumTicketStatus status)
        {
            var result = await _ticketService.ChangeTicketStatus(id, status);
            return Ok(new { ticketId = result });
        }


        [HttpGet("comments/{id}")]
        public async Task<IActionResult> GetComments(int id)
        {
            return Ok(await _ticketService.GetComments(id));
        }
        [HttpPost("comment/{ticketId}")]
        public async Task<IActionResult> AddComment(int ticketId, string comment, [FromBody] List<int> taggedUsers = null)
        {
            var result = await _ticketService.AddComment(ticketId, comment, taggedUsers);
            return Ok(new { ticketId = result });
        }
        [HttpPut("comment")]
        public async Task<IActionResult> UpdateComment(int id, string comment)
        {
            var result = await _ticketService.UpdateComment(id, comment);
            return Ok(new { ticketId = result });
        }

        [HttpDelete("comment/{tickedId}")]
        public async Task<IActionResult> DeleteComment(int tickedId, int commentId)
        {
            return Ok(await _ticketService.DeleteComment(tickedId, commentId));
        }
        [HttpGet("tiles")]
        public IActionResult GetTile(int companyId, TicketFilterInputDto input)
        {
            var result = _ticketService.GetTilesView(companyId,input);
            return Ok(result);
        }
        #endregion


    }
}