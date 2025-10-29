using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        private readonly ITicketService _ticketService;
        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
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






        // view sections apis

        #region Ticket Full Page and Project/Customer Details 1
        [HttpGet("basic-detail/{id}")]
        public async Task<IActionResult> TicketBasicDetails(int id)
        {
            return Ok(await _ticketService.GetBasicDetails(id));
        }
        #endregion

        #region Ticket Full Page and Project/Customer Details 1
        [HttpPut("basic-detail/{id}")]
        public async Task<IActionResult> UpdateBasicDetails(int id, TicketBasicDetailInputDto input)
        {
            var result = await _ticketService.UpdateBasicDetails(id, input);
            return Ok(new { ticketId = result });
        }
        #endregion
        //[HttpGet("ticket-specification/{id}")]
        //public async Task<IActionResult> TicketSpecification(int id)
        //{
        //    var result = await _ticketService.TicketView(id);
        //    return Ok(new { ticketId = result });
        //}
        //[HttpGet("linking-items/{id}")]
        //public async Task<IActionResult> TicketView(int id)
        //{
        //    var result = await _ticketService.TicketView(id);
        //    return Ok(new { ticketId = result });
        //}
        //#endregion

        //#region Ticket SubFrom
        //[HttpGet("aditional-data/{id}")]
        //public async Task<IActionResult> TicketView(int id)
        //{
        //    var result = await _ticketService.TicketView(id);
        //    return Ok(new { ticketId = result });
        //}
        //#endregion

        //#region Ticket Tab Pages
        //[HttpGet("change-log{id}")]
        //public async Task<IActionResult> ChangeLogHistories(int id)
        //{
        //    var result = await _ticketService.TicketView(id);
        //    return Ok(new { ticketId = result });
        //}
        //[HttpGet("comments/{id}")]
        //public async Task<IActionResult> Comments(int id)
        //{
        //    var result = await _ticketService.TicketView(id);
        //    return Ok(new { ticketId = result });
        //}
        //[HttpGet("files/{id}")]
        //public async Task<IActionResult> Attachments(int id)
        //{
        //    var result = await _ticketService.TicketView(id);
        //    return Ok(new { ticketId = result });
        //}
        //[HttpGet("watchers/{id}")]
        //public async Task<IActionResult> Watchers(int id)
        //{
        //    var result = await _ticketService.TicketView(id);
        //    return Ok(new { ticketId = result });
        //}
        //#endregion



        // Ticket update apis
        //[HttpPut("basic")]
        //public IActionResult UpdateProject(int ticketId, TicketFilterInputDto input)
        //{
        //    var result = _ticketService.GetTilesView(companyId, input);
        //    return Ok(result);
        //}


        //[HttpPut("root-cause-resulation")]
        //public IActionResult UpdateProject(int ticketId, TicketFilterInputDto input)
        //{
        //    var result = _ticketService.GetTilesView(companyId, input);
        //    return Ok(result);
        //}

        //[HttpPut("root-cause-resulation")]
        //public IActionResult UpdateProject(int ticketId, TicketFilterInputDto input)
        //{
        //    var result = _ticketService.GetTilesView(companyId, input);
        //    return Ok(result);
        //}
        //[HttpPut("project")]
        //public IActionResult UpdateProject(int ticketId, TicketFilterInputDto input)
        //{
        //    var result = _ticketService.GetTilesView(companyId, input);
        //    return Ok(result);
        //}
        //[HttpPut("customer")]
        //public IActionResult UpdateProject(int ticketId, TicketFilterInputDto input)
        //{
        //    var result = _ticketService.GetTilesView(companyId, input);
        //    return Ok(result);
        //}


        //[HttpPut("sub-form")]
        //public IActionResult SaveSubForm(int ticketId, TicketFilterInputDto input)
        //{
        //    var result = _ticketService.GetTilesView(companyId, input);
        //    return Ok(result);
        //}

        [HttpGet("tiles")]
        public IActionResult GetTile(int companyId, TicketFilterInputDto input)
        {
            var result = _ticketService.GetTilesView(companyId,input);
            return Ok(result);
        }
    }
}