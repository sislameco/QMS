using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Dto.Org;
using Models.Dto.Ticket;
using Models.Enum;
using Services.IssueManagement;
using WebApi.Helper.Security;

namespace WebApi.Controllers.IssueManagement
{
    [ApiController]
    [Route("ticket")]
    //[CustomAuthorization]
    [AllowAnonymous]
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
        [HttpGet("linking-item/{id}")]
        public async Task<IActionResult> GetLinkingItems(int id)
        {
            return Ok(await _ticketService.GetLinkingItems(id));
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



        #region Delete Ticket Item
        [HttpGet("watcher/{tickedId}")]
        public async Task<IActionResult> DeleteWatcher(int tickedId, int watcherId)
        {
            return Ok(await _ticketService.DeleteWatcher(tickedId, watcherId));
        }

        #endregion
        #region Ticket Full Page and Project/Customer Details 1
        [HttpPut("basic-detail/{id}")]
        public async Task<IActionResult> UpdateBasicDetails(int id, TicketBasicDetailInputDto input)
        {
            var result = await _ticketService.UpdateBasicDetails(id, input);
            return Ok(new { ticketId = result });
        }

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
        public async Task<IActionResult> AddComment(int ticketId, string comment)
        {
            var result = await _ticketService.AddComment(ticketId, comment);
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