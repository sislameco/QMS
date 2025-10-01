using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Dto.Org;
using System.Threading.Tasks;
using System.Collections.Generic;
using Services.CompanyConfig;

namespace WebApi.Controllers.CompanyConfig
{
    [ApiController]
    [Route("sla")]
    [AllowAnonymous]
    public class SLAController : ControllerBase
    {
        private readonly ISLAService _slaService;

        public SLAController(ISLAService slaService)
        {
            _slaService = slaService;
        }

        // Get all SLAs
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var slas = await _slaService.GetAllAsync();
            return Ok(slas);
        }

        // Get SLA by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var sla = await _slaService.GetByIdAsync(id);
            if (sla == null) return NotFound();
            return Ok(sla);
        }

        // Create SLA
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SLAInputDto dto)
        {
            var created = await _slaService.CreateAsync(dto);
            return Ok(created);
        }

        // Update SLA
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] SLAInputDto dto)
        {
            var updated = await _slaService.UpdateAsync(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        // Delete SLA
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _slaService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
