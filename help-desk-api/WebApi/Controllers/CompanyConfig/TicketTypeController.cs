using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Dto.Org;
using System.Threading.Tasks;
using System.Collections.Generic;
using Services.CompanyConfig;

namespace WebApi.Controllers.CompanyConfig
{
    [ApiController]
    [Route("ticket-type")]
    [AllowAnonymous]
    public class TicketTypeController : ControllerBase
    {
        private readonly ITicketTypeService _ticketTypeService;

        public TicketTypeController(ITicketTypeService ticketTypeService)
        {
            _ticketTypeService = ticketTypeService;
        }

        // Get all SLAs
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var slas = await _ticketTypeService.GetAllAsync();
            return Ok(slas);
        }

        // Get SLA by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var sla = await _ticketTypeService.GetByIdAsync(id);
            if (sla == null) return NotFound();
            return Ok(sla);
        }

        // Create SLA
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SLAInputDto dto)
        {
            var created = await _ticketTypeService.CreateAsync(dto);
            return Ok(created);
        }

        // Update SLA
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] SLAInputDto dto)
        {
            var updated = await _ticketTypeService.UpdateAsync(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        // Delete SLA
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _ticketTypeService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
