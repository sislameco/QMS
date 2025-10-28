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

        [HttpPost]
        public async Task<ActionResult<bool>> CreateMany([FromBody] CustomFieldInputDto input)
        {

            var createdFields = await _customFieldService.CreateManyAsync(input);
            return CreatedAtAction(nameof(GetAll), createdFields);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CustomFieldInputDto>> Update(int id, [FromBody] CustomFieldInputDto dto)
        {
            var updated = await _customFieldService.UpdateAsync(id, dto);
            if (updated == null)
                return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _customFieldService.DeleteAsync(id);
            return Ok(deleted);
        }


        [HttpPatch("display-order")]
        public async Task<IActionResult> ChangeDisplayOrder([FromBody]  List<FieldDisplayOrderInputDto> input)
        {
            return Ok(await _customFieldService.DisplayOrder(input));
        }

        [HttpGet("ticket-type")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<CustomFieldInputDto>>> GetTicketTypes(int companyId)
        {
            var fields = await _customFieldService.GetTicketTypesByFiled(companyId);
            return Ok(fields);
        }
    }
}
