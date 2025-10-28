using Microsoft.AspNetCore.Http;
using Models.AppSettings;
using Models.Dto.Auth;
using Models.Dto.GlobalDto;
using Models.Dto.Menus;
using Models.Entities.Audit;
using Models.Entities.Auth;
using Models.Entities.UserManagement;
using Models.Enum;
using Repository;
using Repository.Repo.Permission;
using Services.Email;
using StackExchange.Redis;
using System.Net;
using Utilities.Redis;
using Utils;
using Utils.EmailUtil;
using Utils.Exceptions;
using Utils.JWT;
using Utils.LoginData;

namespace Services.AuthService
{
    public interface IHelpDeskAuthService
    {
        Task<HelpDeskLoginResponseDto> LoginAsync(HelpDeskLoginDto dto, HttpContext httpContext, HttpRequest request);
        Task<HelpDeskLoginResponseDto> LoginAsync(HelpDeskIntregationLoginDto userId, HttpContext httpContext, HttpRequest request);
        bool SignOut();
        Task<HelpDeskLoginResponseDto> RefreshToken(string token, HttpContext httpContext, HttpRequest request);
        Task<string> ForgotPassword(ForgotPasswordInputDto model);
        Task<ObjectResponse<bool>> UpdatePassword(ChangePasswordInputDto data, int userId);
        Task<int> RecoverPasswordVerification(RecoverPasswordVerificationInputDto model);
    }

    public class HelpDeskAuthService : IHelpDeskAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtGenerator _jwtGenerator;
        private readonly IUserInfos _user;
        private readonly IMenuRepository _menuRepository;
        private readonly IEmailNotificationService _emailNotificationService;
        public HelpDeskAuthService(IUnitOfWork unitOfWork, IJwtGenerator jwtGenerator, IUserInfos user, IMenuRepository menuRepository, IEmailNotificationService emailNotificationService)
        {
            _unitOfWork = unitOfWork;
            _jwtGenerator = jwtGenerator;
            _user = user;
            _menuRepository = menuRepository;
            _emailNotificationService = emailNotificationService;
        }
        //*7wTu/0DUo
        public async Task<HelpDeskLoginResponseDto> LoginAsync(HelpDeskLoginDto dto, HttpContext httpContext, HttpRequest request)
        {
            string Password = Common.EncryptText(dto.Password);
            // 1. Fetch user by email
            var user = await _unitOfWork.Repository<UserModel, int>().FirstOrDefaultAsync(u => u.UserName == dto.Email && u.PasswordHash == Password 
           //&& u.IsActive == true
            );

            if (user == null)
            {
                throw new BadRequestException("Invalid username or password.");
            }

            // 2. Check if password change is required
            //if (user.LastPasswordChange.AddDays(90) <= DateTime.UtcNow)
            //    return new HelpDeskLoginResponseDto { IsPasswordChange = false, UserId = user.Id };

            var menus = await _menuRepository.GetPermittedActions(user.Id);

            //if (!menus.Any())
            //    throw new BadRequestException("You have no permitted menus!");

            var menu_cache_key = $"{user.Id}";
            AuthCacheUtil.SetPermittedMenu(menu_cache_key, menus);

            //// 3. Verify password
            dto.Browser = Common.GetBrowserIpInformation(httpContext, request);
            var operatingSystem = Convert.ToString(httpContext.Request?.Headers["OperatingSystem"]);
            var Browser = dto.Browser.Item1.UA.ToString();
            var machine_user = dto.Browser.Item1.Device.ToString();
            var UserHostAddress = dto.Browser.Item2.ToString();
            var loginId = await SaveUserLogin(user.Id, UserHostAddress, Browser, machine_user);

            string token = "";
            string refreshToken = "";

            int tokenValidaty = 480; //minutes;
            int refreshTokenValidity = 1;

            if (operatingSystem == "android" || operatingSystem == "ios")
            {
                tokenValidaty = 365 * 24 * 60; //minutes
                refreshTokenValidity = tokenValidaty * 2;//days
            }
            if (user != null)
            {
                token = _jwtGenerator.GenerateJWT(user, UserHostAddress, loginId);
                refreshToken = await CreateRefreshToken(user.Id, UserHostAddress, loginId, refreshTokenValidity);
            }

            // 4. Return success response with token placeholder
            return new HelpDeskLoginResponseDto
            {
                Success = true,
                Message = "Login successful.",
                Token = token,
                RefreshToken = refreshToken,
                IsPasswordChange = true,
                UserId = user.Id
            };
        }
        public async Task<HelpDeskLoginResponseDto> LoginAsync(HelpDeskIntregationLoginDto input, HttpContext httpContext, HttpRequest request)
        {
            // 1. Fetch user by email
            var user = await _unitOfWork.Repository<UserModel, int>().FirstOrDefaultAsync(u => u.IntegrationsPrimaryId == input.UserId
            );

            if (user == null)
            {
                // Qlogger.LogError("User not found for embaded login with userId: " + userId);
                throw new BadRequestException("Invalid username or password.");
            }

            // 2. Check if password change is required
            //if (user.LastPasswordChange.AddDays(90) <= DateTime.UtcNow)
            //    return new HelpDeskLoginResponseDto { IsPasswordChange = false, UserId = user.Id };

            var menus = await _menuRepository.GetPermittedActions(user.Id);

            //if (!menus.Any())
            //    throw new BadRequestException("You have no permitted menus!");

            var menu_cache_key = $"{user.Id}";
            AuthCacheUtil.SetPermittedMenu(menu_cache_key, menus);

            //// 3. Verify password
            input.Browser = Common.GetBrowserIpInformation(httpContext, request);
            var operatingSystem = Convert.ToString(httpContext.Request?.Headers["OperatingSystem"]);
            var Browser = input.Browser.Item1.UA.ToString();
            var machine_user = input.Browser.Item1.Device.ToString();
            var UserHostAddress = input.Browser.Item2.ToString();
            var loginId = await SaveUserLogin(user.Id, UserHostAddress, Browser, machine_user);

            string token = "";
            string refreshToken = "";

            int tokenValidaty = 480; //minutes;
            int refreshTokenValidity = 1;

            if (operatingSystem == "android" || operatingSystem == "ios")
            {
                tokenValidaty = 365 * 24 * 60; //minutes
                refreshTokenValidity = tokenValidaty * 2;//days
            }
            if (user != null)
            {
                token = _jwtGenerator.GenerateJWT(user, UserHostAddress, loginId);
                refreshToken = await CreateRefreshToken(user.Id, UserHostAddress, loginId, refreshTokenValidity);
            }

            // 4. Return success response with token placeholder
            return new HelpDeskLoginResponseDto
            {
                Success = true,
                Message = "Login successful.",
                Token = token,
                RefreshToken = refreshToken,
                IsPasswordChange = true,
                UserId = user.Id
            };
        }
        private async Task<int> SaveUserLogin(int userid, string ip, string browser, string machine_user)
        {
            UserLoginModel userLoginObj = new UserLoginModel()
            {
                FkUserId = (int)userid,
                IpAddress = ip,
                Browser = browser,
                MachineUser = machine_user,
                LoginTime = DateTime.UtcNow,
            };
            await _unitOfWork.WithOutRepository<UserLoginModel, int>().AddAsync(userLoginObj);
            await _unitOfWork.CommitAsync();
            return userLoginObj.Id;
        }
        public bool SignOut()
        {
            _user.SetUserId(0);
            return true;
        }
        public async Task<HelpDeskLoginResponseDto> RefreshToken(string token, HttpContext httpContext, HttpRequest request)
        {
            var browser = Common.GetBrowserIpInformation(httpContext, request);
            var userHostAddress = browser.Item2.ToString();
            var operatingSystem = Convert.ToString(httpContext.Request?.Headers["OperatingSystem"]);
            /*
             Get Refresh token from repository to generate a new access token
             */
            RefreshTokenModel currentToken = await GetRefreshToken(token, userHostAddress);
            if (currentToken == null)
                throw new SessionExpiredException("Invalid token request!");

            UserModel userInfo = await _unitOfWork.Repository<UserModel, int>().FirstOrDefaultAsync(s => s.Id == currentToken.FkUserId && s.RStatus == EnumRStatus.Active);
            if (userInfo == null)
                throw new SessionExpiredException("Invalid token request!");
            /*
             Generate new access token using refresh token
             */
            var newToken = _jwtGenerator.GenerateJWT(userInfo, userHostAddress, currentToken.FkLoginId);

            /* Getting permitted menus again to update Redis cache 
             */
            var menus = await _menuRepository.GetUserPermittedMenusAsync(userInfo.Id);

            if (menus.Count == 0)
                throw new SessionExpiredException("Invalid token request!");

            //  AuthCacheUtil.SetPermittedMenu($"{userInfo.Id}", menus);

            var refreshToken = await CreateRefreshToken(userInfo.Id, userHostAddress, currentToken.FkLoginId);
            DeleteRefreshToken(currentToken);
            return new HelpDeskLoginResponseDto { Token = newToken, RefreshToken = refreshToken };
        }
        public async Task<RefreshTokenModel> GetRefreshToken(string token, string userIp)
        {
            return await _unitOfWork.WithOutRepository<RefreshTokenModel, int>()
                .FirstOrDefaultAsync(rt => rt.Token == token && rt.UserIp == userIp);
        }
        public async Task<string> CreateRefreshToken(int userId, string userIp, int loginId, int validaty = 1)
        {
            var token = new RefreshTokenModel
            {
                FkUserId = userId,
                Token = Guid.NewGuid().ToString(),
                ExpiresAt = DateTime.UtcNow.AddDays(validaty),
                UserIp = userIp,
                FkLoginId = loginId
            };

            await _unitOfWork.WithOutRepository<RefreshTokenModel, int>().AddAsync(token);
            await _unitOfWork.CommitAsync();
            return token.Token;
        }
        private UserModel GetUserInfoById(int userId)
        {
            // Fetch user by ID using the UnitOfWork's generic repository
            return _unitOfWork.Repository<UserModel, int>().GetByIdAsync(userId).GetAwaiter().GetResult();
        }
        public async void DeleteRefreshToken(RefreshTokenModel token)
        {
            await _unitOfWork.WithOutRepository<RefreshTokenModel, int>().SoftDeleteAsync(token);
            await _unitOfWork.CommitAsync();
        }

        public async Task<string> ForgotPassword(ForgotPasswordInputDto model)
        {
            UserModel user = await _unitOfWork.Repository<UserModel, int>().FirstOrDefaultAsync(u => u.UserName == model.UserName) ?? throw new BadRequestException("");

            if (user == null || string.IsNullOrEmpty(user.Email))
                throw new BadRequestException("No User Found");

            var token = string.Empty;

            if (user.RStatus == EnumRStatus.Active)
            {
                // get template and generate token

                return await CreateTokenOtpAndSendEmail(user, model);
                // send opt in email
            }
            else
            {
                throw new BadRequestException("Inactive User");
            }
            return token;
        }
        public async Task<ObjectResponse<bool>> UpdatePassword(ChangePasswordInputDto data, int userId)
        {
            var user = await _unitOfWork.Repository<UserModel, int>().FirstOrDefaultAsync(x => x.Id == userId) ?? throw new BadRequestException("") ?? throw new BadRequestException("Requested user does not exist.");


            //if (Common.DecryptText(user.PasswordHash) != data.CurrentPassword)
            //    throw new BadRequestException("Entered Current password is not valid.");

            user.PasswordHash = Common.EncryptText(data.NewPassword);
            await _unitOfWork.Repository<UserModel, int>().UpdateAsync(user);
            await _unitOfWork.CommitAsync();
            return new ObjectResponse<bool> { Data = true };
        }

        public async Task<int> RecoverPasswordVerification(RecoverPasswordVerificationInputDto model)
        {
            JwtTokenParse userJwtToken = JwtTokenDecode.GetTokenDecode(model.UserToken, true);
            var userId = Convert.ToInt32(userJwtToken.UserId);

            var recoverPasswordToken = await _unitOfWork.Repository<RecoverPasswordTokenModel,int>().FirstOrDefaultAsync(r => r.FkUserId == userId && r.OtpCode == model.Code);

            if (recoverPasswordToken != null)
            {
                recoverPasswordToken.IsVarified = true;
                await _unitOfWork.Repository<RecoverPasswordTokenModel, int>().UpdateAsync(recoverPasswordToken);
                await _unitOfWork.CommitAsync();
                return userId;
            }
            else
            {
                throw new BadRequestException("Invalid OTP!");
            }
        }

        private async Task<string> CreateTokenOtpAndSendEmail(UserModel user, ForgotPasswordInputDto model)
        {
            var UserHostAddress = model.Browser.Item2.ToString();
            Random generator = new Random();
            string otpCode = generator.Next(0, 100000).ToString("D6");
            var token = _jwtGenerator.GenerateJWT(user, UserHostAddress, user.Id, 15);
            RecoverPasswordTokenModel recoverPasswordToken = new RecoverPasswordTokenModel
            {
                FkUserId = user.Id,
                IsVarified = false,
                OtpCode = otpCode,
                UserToken = token,
                RStatus = EnumRStatus.Active
            };

            // Use UnitOfWork to add and save the token
            await _unitOfWork.WithOutRepository<RecoverPasswordTokenModel, int>().AddAsync(recoverPasswordToken);
            await _unitOfWork.CommitAsync();
            await _emailNotificationService.SendUserPasswordRecoverEmail(user, otpCode);
            return token;
        }
    }
}
