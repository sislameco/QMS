using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Dto.Auth;
using Models.Dto.GlobalDto;
using Models.Enum;
using Services.AuthService;
using System.Threading.Tasks;
using Utils;

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
        public async Task<IActionResult> Login(HelpDeskLoginDto data)
        {
            var response = await _authService.LoginAsync(data, HttpContext,Request);
            return Ok(response);
        }


        [HttpPost]
        [Route("embedded-login")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(HelpDeskLoginResponseDto), 200)]
        public async Task<IActionResult> EmbadedLogin(HelpDeskIntregationLoginDto input, EnumIntregationType app)
        {
            var response = await _authService.LoginAsync(input, HttpContext, Request);
            return Ok(response);
        }


        [HttpGet]
        [Route("sign-out")]
        public async Task<IActionResult> SignOut()
        {
            return Ok(_authService.SignOut());
        }


        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(string token)
        {
            return Ok(await _authService.RefreshToken(token, HttpContext, Request));
        }

        [AllowAnonymous]
        [HttpPost("password-recovery-otp-verification")]
        public async Task<ObjectResponse<int>> RecoverPasswordVerification(RecoverPasswordVerificationInputDto model)
        {
            model.Browser = Common.GetBrowserIpInformation(HttpContext, Request);
            var res = await _authService.RecoverPasswordVerification(model);
            return new ObjectResponse<int>() { Data = res, Message = res >  0 ? "Successfully verified" : "Verification failed" };
        }

        [AllowAnonymous]
        [HttpPost("forgot-password")]
        public async Task<ObjectResponse<string>> ForgotPassword(ForgotPasswordInputDto model)
        {
            model.Browser = Common.GetBrowserIpInformation(HttpContext, Request);
            var res = await _authService.ForgotPassword(model);
            return new ObjectResponse<string>() { Data = res, Message = "Send OTP" };
        }

        [HttpPost]
        [Route("change-password/{userId}")]
        [AllowAnonymous]
        public async Task<ObjectResponse<bool>> ChangePassword(ChangePasswordInputDto data, int userId)
        {
            return await _authService.UpdatePassword(data, userId);
        }

    }
}
