using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Dto.Org;
using Services.Org;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebApi.Controllers.Org
{
    [ApiController]
    [Route("sla")]
    public class SLAController : ControllerBase
    {
        private readonly ISLAService _slaService;

        public SLAController(ISLAService slaService)
        {
            _slaService = slaService;
        }

        // Get all SLAs
        [HttpGet]
        public async Task<IActionResult> GetAll(int fkCompanyId)
        {
            var slas = await _slaService.GetAllAsync(fkCompanyId);
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
            bool deleted = await _slaService.DeleteAsync(id);
            return Ok(deleted);
        }

        [HttpGet("tiles")]
        public  IActionResult GetTile(int fkCompanyId)
        {
            var slas = _slaService.GetTile(fkCompanyId);
            return Ok(slas);
        }
    }
}
