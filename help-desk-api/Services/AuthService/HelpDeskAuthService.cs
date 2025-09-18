using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Models.Dto.Auth;
using Models.Entities.Audit;
using Models.Entities.UserManagement;
using Repository;
using Utils;

namespace Services.AuthService
{
    public interface IHelpDeskAuthService
    {
        Task<HelpDeskLoginResponseDto> LoginAsync(HelpDeskLoginDto dto, HttpContext httpContext, HttpRequest request);
    }

    public class HelpDeskAuthService : IHelpDeskAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PasswordHasher<UserModel> _passwordHasher;
        private readonly IJwtGenerator _jwtGenerator;
        public HelpDeskAuthService(IUnitOfWork unitOfWork, IJwtGenerator jwtGenerator)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = new PasswordHasher<UserModel>();
            _jwtGenerator = jwtGenerator;
        }
        //*7wTu/0DUo
        public async Task<HelpDeskLoginResponseDto> LoginAsync(HelpDeskLoginDto dto, HttpContext httpContext, HttpRequest request)
        {
            string Password = Common.EncryptText(dto.Password);
            // 1. Fetch user by email
            var user = await _unitOfWork.Repository<UserModel, long>().FirstOrDefaultAsync(u => u.UserName == dto.Email && u.PasswordHash == Password);

            if (user == null)
            {
                return new HelpDeskLoginResponseDto
                {
                    Success = false,
                    Message = "Invalid email or password."
                };
            }
            //// 2. Verify password
            dto.Browser = Common.GetBrowserIpInformation(httpContext, request);
            var operatingSystem = Convert.ToString(httpContext.Request?.Headers["OperatingSystem"]);
            var Browser = dto.Browser.Item1.UA.ToString();
            var machine_user = dto.Browser.Item1.Device.ToString();
            var UserHostAddress = dto.Browser.Item2.ToString();
            var loginId = await SaveUserLogin(user.Id, UserHostAddress, Browser, machine_user);

            string token="";
            if (user!=null)
                token = _jwtGenerator.GenerateJWT(user, UserHostAddress, loginId);
            // 3. Return success response with token placeholder
            return new HelpDeskLoginResponseDto
            {
                Success = true,
                Message = "Login successful.",
                Token = token,
                UserId = user.Id
            };
        }
        private async Task<long> SaveUserLogin(long userid, string ip, string browser, string machine_user)
        {
            UserLoginModel userLoginObj = new UserLoginModel()
            {
                FkUserId = (int)userid,
                IpAddress = ip,
                Browser = browser,
                MachineUser = machine_user,
                LoginTime = DateTime.UtcNow,
            };
            await _unitOfWork.Repository<UserLoginModel, long>().AddAsync(userLoginObj);
            await _unitOfWork.CommitAsync();
            return userLoginObj.Id;
        }
    }
}
