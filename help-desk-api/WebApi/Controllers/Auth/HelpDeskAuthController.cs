using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Dto.Auth;
using Services.AuthService;
using System.Threading.Tasks;

namespace WebApi.Controllers.Auth
{
    [ApiController]
    [Route("helpdesk")]
    public class HelpDeskAuthController : ControllerBase
    {
        private readonly IHelpDeskAuthService _authService;
        public HelpDeskAuthController(IHelpDeskAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Login to HelpDesk.
        /// </summary>
        /// <param name="dto">Login DTO</param>
        /// <returns>Login response</returns>
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(HelpDeskLoginResponseDto), 200)]
        public async Task<IActionResult> Login([FromBody] HelpDeskLoginDto dto)
        {
            var response = await _authService.LoginAsync(dto,HttpContext,Request);
            return Ok(response);
        }
    }
}
