using Microsoft.AspNetCore.Http;
using Models.Dto.Auth;
using Models.Dto.GlobalDto;
using Models.Dto.Menus;
using Models.Entities.Audit;
using Models.Entities.UserManagement;
using Models.Enum;
using Repository;
using Repository.Repo.Permission;
using StackExchange.Redis;
using System.Net;
using Utilities.Redis;
using Utils;
using Utils.Exceptions;
using Utils.LoginData;

namespace Services.AuthService
{
    public interface IHelpDeskAuthService
    {
        Task<HelpDeskLoginResponseDto> LoginAsync(HelpDeskLoginDto dto, HttpContext httpContext, HttpRequest request);
        bool SignOut();
        Task<HelpDeskLoginResponseDto> RefreshToken(string token,  HttpContext httpContext, HttpRequest request);
        Task<string> ForgotPassword(ForgotPasswordInputDto model);
        Task<ObjectResponse<bool>> UpdatePassword(ChangePasswordInputDto data, int userId);
    }

    public class HelpDeskAuthService : IHelpDeskAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtGenerator _jwtGenerator;
        private readonly IUserInfos _user;
        private readonly IMenuRepository _menuRepository;
        public HelpDeskAuthService(IUnitOfWork unitOfWork, IJwtGenerator jwtGenerator, IUserInfos user, IMenuRepository menuRepository)
        {
            _unitOfWork = unitOfWork;
            _jwtGenerator = jwtGenerator;
            _user = user;
            _menuRepository = menuRepository;
        }
        //*7wTu/0DUo
        public async Task<HelpDeskLoginResponseDto> LoginAsync(HelpDeskLoginDto dto, HttpContext httpContext, HttpRequest request)
        {
            string Password = Common.EncryptText(dto.Password);
            // 1. Fetch user by email
            var user = await _unitOfWork.Repository<UserModel, int>().FirstOrDefaultAsync(u => u.UserName == dto.Email && u.PasswordHash == Password);

            if (user == null)
            {
                return new HelpDeskLoginResponseDto
                {
                    Success = false,
                    Message = "Invalid email or password."
                };
            }

            // 2. Check if password change is required
            //if (user.LastPasswordChange.AddDays(90) <= DateTime.UtcNow)
            //    return new HelpDeskLoginResponseDto { IsPasswordChange = false, UserId = user.Id };

            var menus = await _menuRepository.GetUserPermittedMenusAsync(user.Id);

            //if (!menus.Any())
            //    throw new BadRequestException("You have no permitted menus!");

            var menu_cache_key = $"{user.Id}";
            //AuthCacheUtil.SetPermittedMenu(menu_cache_key, menus);

            //// 3. Verify password
            dto.Browser = Common.GetBrowserIpInformation(httpContext, request);
            var operatingSystem = Convert.ToString(httpContext.Request?.Headers["OperatingSystem"]);
            var Browser = dto.Browser.Item1.UA.ToString();
            var machine_user = dto.Browser.Item1.Device.ToString();
            var UserHostAddress = dto.Browser.Item2.ToString();
            var loginId = await SaveUserLogin(user.Id, UserHostAddress, Browser, machine_user);

            string token="";
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

            var operatingSystem = Convert.ToString(httpContext.Request?.Headers["OperatingSystem"]);
            /*
             Get Refresh token from repository to generate a new access token
             */
            RefreshTokenModel currentToken = await GetRefreshToken(token, ip);
            if (currentToken == null)
                throw new SessionExpiredException("Invalid token request!");

            UserModel userInfo = await _unitOfWork.Repository<UserModel, int>().FirstOrDefaultAsync(s=> s.Id == currentToken.FkUserId && s.RStatus == EnumRStatus.Active);
            if (userInfo == null)
                throw new SessionExpiredException("Invalid token request!");
            /*
             Generate new access token using refresh token
             */
            var newToken = _jwtGenerator.GenerateJWT(userInfo, browser.Item2.ipa, currentToken.FkLoginId);

            /* Getting permitted menus again to update Redis cache 
             */
            var menus = await _menuRepository.GetUserPermittedMenusAsync(userInfo.Id);

            if (menus.Count == 0)
                throw new SessionExpiredException("Invalid token request!");

          //  AuthCacheUtil.SetPermittedMenu($"{userInfo.Id}", menus);

            var refreshToken = await CreateRefreshToken(userInfo.Id, ip, currentToken.FkLoginId);
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
            UserModel user = await _unitOfWork.Repository<UserModel,int>().FirstOrDefaultAsync(u => u.UserName == model.UserName) ?? throw new BadRequestException("");

            if (user == null || string.IsNullOrEmpty(user.Email))
                throw new BadRequestException("No User Found");

            var token = string.Empty;

            if (user.RStatus == EnumRStatus.Active)
            {
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

  
            if (Common.DecryptText(user.PasswordHash) != data.CurrentPassword)
                throw new BadRequestException("Entered Current password is not valid.");

            // update password
            //send an email notification

            return new ObjectResponse<bool> { Data = true };
        }
    }
}
