using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.CompanyConfig
{
    [ApiController]
    [Route("company")]
    [AllowAnonymous]
    public class SLAController: ControllerBase
    {
    }
}
