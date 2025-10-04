using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Dto.Org;
using Services.Org;
using WebApi.Helper.Security;

namespace WebApi.Controllers.Org
{
    [ApiController]
    [Route("department-setting")]
    [AllowAnonymous]
    public class DepartmentController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        public DepartmentController(ICompanyService companyService)
        {
            _companyService = companyService;
        }
        [HttpGet("all/{companyId}")]
        public async Task<IActionResult> GetAllDepartments(int companyId, [FromQuery] DepartmentSettingInputDto input)
        {
            var departments = await _companyService.GetAllDepartmentsAsync(companyId, input);
            return Ok(departments);
        }
        [HttpPatch("update")]
        public async Task<IActionResult> UpdateDepartment([FromBody] DepartmentUpdateDto dto)
        {
            var result = await _companyService.UpdateDepartmentAsync(dto);
            if (result)
                return Ok();
            return BadRequest("Update failed");
        }
    }
}
