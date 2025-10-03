using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Dto.Org;
using Services.Dashboard;
using Services.Org;
using System.Threading.Tasks;

namespace WebApi.Controllers.Org
{
    [ApiController]
    [Route("company")]
    [AllowAnonymous]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetActiveCompanies()
        {
            var companies = await _companyService.GetActiveCompaniesAsync();
            return Ok(companies);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCompany(int id, [FromBody] CompanyDto dto)
        {
            return Ok(await _companyService.UpdateCompanyAsync(id, dto));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompany(int id)
        {
            return Ok(await _companyService.GetCompany(id));
        }
    }
}
