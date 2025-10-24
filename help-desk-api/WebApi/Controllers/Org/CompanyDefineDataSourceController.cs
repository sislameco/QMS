using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Setup;
using Models.Dto.CustomDefine;
using Models.Enum;

namespace WebApi.Controllers.Org
{
    [ApiController]
    [Route("root-resolution")]
    public class CompanyDefineDataSourceController : ControllerBase
    {
        private readonly ICompanyDefineRootResolutionService _rootResolutionService;

        public CompanyDefineDataSourceController(ICompanyDefineRootResolutionService rootResolutionService)
        {
            _rootResolutionService = rootResolutionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int companyId, [FromQuery] EnumRootResolutionType type)
        {
            var result = await _rootResolutionService.GetAllAsync(companyId, type);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _rootResolutionService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody] RootCauseInputDto input)
        {
            var success = await _rootResolutionService.SaveAsync(input);
            if (success) return Ok();
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _rootResolutionService.DeleteAsync(id);
            if (success) return Ok();
            return NotFound();
        }

        [HttpPut("displayorder")]
        public async Task<IActionResult> ChangeDisplayOrder([FromQuery] int id, [FromQuery] int order)
        {
            var success = await _rootResolutionService.ChangeDisplayOrder(id, order);
            if (success) return Ok(success);
            return NotFound();
        }
    }
}
