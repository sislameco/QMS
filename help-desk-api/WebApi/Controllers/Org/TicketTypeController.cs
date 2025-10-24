using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Dto.Org;
using Services.Org;

namespace WebApi.Controllers.Org
{
    [ApiController]
    [Route("ticket-type")]
    public class TicketTypeController : ControllerBase
    {
        private readonly ITicketTypeService _ticketTypeService;

        public TicketTypeController(ITicketTypeService ticketTypeService)
        {
            _ticketTypeService = ticketTypeService;
        }

        // Get all TicketType
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var slas = await _ticketTypeService.GetAllAsync();
            return Ok(slas);
        }

        // Get TicketType by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var sla = await _ticketTypeService.GetByIdAsync(id);
            return Ok(sla);
        }

        // Create TicketType
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TicketTypeInputDto dto)
        {
            var created = await _ticketTypeService.CreateAsync(dto);
            return Ok(created);
        }

        // Update TicketType
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TicketTypeInputDto dto)
        {
            var updated = await _ticketTypeService.UpdateAsync(id, dto);
            return Ok(updated);
        }

        // Delete TicketType
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _ticketTypeService.DeleteAsync(id);
            return Ok(deleted);
        }
    }
}
