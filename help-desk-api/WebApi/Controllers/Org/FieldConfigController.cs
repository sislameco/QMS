using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Dto.Org;
using Services.Org;

namespace WebApi.Controllers.Org
{
    [ApiController]
    [Route("field")]
    [AllowAnonymous]
    public class FieldConfigController : ControllerBase
    {
        private readonly ICustomFieldService _customFieldService;

        public FieldConfigController(ICustomFieldService customFieldService)
        {
            _customFieldService = customFieldService;
        }

        // GET: field
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomFieldInputDto>>> GetAll()
        {
            var fields = await _customFieldService.GetAllAsync();
            return Ok(fields);
        }

        // GET: field/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<CustomFieldOutPutDto>> GetById(int id)
        {
            var field = await _customFieldService.GetByIdAsync(id);
            if (field == null)
                return NotFound();
            return Ok(field);
        }

        // POST: field
        // Save multiple fields, input is List<CustomFieldDto> (ignore BaseEntity)
        [HttpPost]
        public async Task<ActionResult<bool>> CreateMany([FromBody] CustomFieldInputDto input)
        {

            var createdFields = await _customFieldService.CreateManyAsync(input);
            return CreatedAtAction(nameof(GetAll), createdFields);
        }

        // PUT: field/{id}
        [HttpPut("{id:int}")]
        public async Task<ActionResult<CustomFieldInputDto>> Update(int id, [FromBody] CustomFieldInputDto dto)
        {
            var updated = await _customFieldService.UpdateAsync(id, dto);
            if (updated == null)
                return NotFound();
            return Ok(updated);
        }

        // DELETE: field/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _customFieldService.DeleteAsync(id);
            if (!deleted)
                return NotFound();
            return NoContent();
        }
    }
}
