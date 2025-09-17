using System.Threading.Tasks;
using Models.Dto.Auth;
using Models.Entities.UserManagement;
using Repository;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace Services.AuthService
{
    public interface IHelpDeskAuthService
    {
        Task<HelpDeskLoginResponseDto> LoginAsync(HelpDeskLoginDto dto);
    }

    public class HelpDeskAuthService : IHelpDeskAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PasswordHasher<UserModel> _passwordHasher;

        public HelpDeskAuthService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = new PasswordHasher<UserModel>();
        }

        public async Task<HelpDeskLoginResponseDto> LoginAsync(HelpDeskLoginDto dto)
        {
            // 1. Fetch user by email
            var users = await _unitOfWork.Repository<UserModel, long>().FindByConditionAsync(u => u.Email == dto.Email);
            var user = users.FirstOrDefault();
            if (user == null)
            {
                return new HelpDeskLoginResponseDto
                {
                    Success = false,
                    Message = "Invalid email or password."
                };
            }

            // 2. Verify password
            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                return new HelpDeskLoginResponseDto
                {
                    Success = false,
                    Message = "Invalid email or password."
                };
            }

            // 3. Return success response with token placeholder
            return new HelpDeskLoginResponseDto
            {
                Success = true,
                Message = "Login successful.",
                Token = "TOKEN_PLACEHOLDER",
                UserId = user.Id
            };
        }
    }
}
