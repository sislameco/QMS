using Microsoft.EntityFrameworkCore;
using Models.AppSettings;
using Models.Dto.GlobalDto;
using Models.Dto.Org;
using Models.Dto.UserManagement;
using Models.Entities.UserManagement;
using Models.Enum;
using Repository;
using Repository.Repo.UserManagement;
using Services.Email;
using Utils;
using Utils.Exceptions;

namespace Services.UserManagement
{
    public interface IUserService
    {
        Task<UserModel?> GetByIdAsync(int id);
        Task<List<UserOutPutDto>> GetAllAsync(UserFilterDto inputDto);
        Task AddAsync(HostUserInputDto user);
        Task UpdateAsync(int userId, HostUserUpdateInputDto user);
        Task DeleteAsync(int id);
        UserModel GetSystemUser();
        Task<List<UserDropdownDto>> GetUserSelectedList(int companyId);
        Task<bool> SendInvitation(int userId);
        Task<bool> AcceptRequest(int userId);
    }
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserMGtRepository _userMGtRepository;
        private readonly IEmailNotificationService _emailNotificationService;
        public UserService(IUnitOfWork unitOfWork, IUserMGtRepository userMGtRepository, IEmailNotificationService emailNotificationService)
        {
            _unitOfWork = unitOfWork;
            _userMGtRepository = userMGtRepository;
            _emailNotificationService = emailNotificationService;
        }

        public async Task<UserModel?> GetByIdAsync(int id)
            => await _unitOfWork.Repository<UserModel, int>().GetByIdAsync(id);

        public async Task<List<UserOutPutDto>> GetAllAsync(UserFilterDto inputDto)
        {
            var data = _userMGtRepository.GetUsers(inputDto);
            return await data.ToListAsync();
        }

        public async Task AddAsync(HostUserInputDto user)
        {
            // assign HostUserInputDto to UserModel
            UserModel add = new UserModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                FullName = $"{user.FirstName} {user.LastName}",
                UserName = user.UserName,
                Email = user.Email,
                Phone = user.Phone,
                PasswordHash = Common.EncryptText(user.PasswordHash),
                FkCompanyId = null,
                IsActive = false
            };
            await _unitOfWork.Repository<UserModel, int>().AddAsync(add);
            await _unitOfWork.CommitAsync();

            UserRoleModel userRole =
                new UserRoleModel
                {
                    FKRoleId = user.RoleId,
                    FKUserId = add.Id,
                    RStatus = EnumRStatus.Active,
                    CreatedBy = -1,
                    CreatedDate = DateTime.UtcNow
                };
            await _emailNotificationService.SendInfitation(add, string.Concat(AppSettings.QMSApi.BaseUrl ,$"/api/user/{add.Id}/request-accept"));
            await _unitOfWork.Repository<UserRoleModel, int>().AddAsync(userRole);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateAsync(int userId, HostUserUpdateInputDto input)
        {
            var user = await _unitOfWork.Repository<UserModel, int>().GetByIdAsync(userId);
            if (user == null) throw new Exception("User not found");
            user.FirstName = input.FirstName;
            user.LastName = input.LastName;
            user.FullName = $"{user.FirstName} {user.LastName}";

            await _unitOfWork.Repository<UserModel, int>().UpdateAsync(user);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _unitOfWork.Repository<UserModel, int>().DeleteAsync(id);
            await _unitOfWork.CommitAsync();
        }
        public UserModel GetSystemUser()
        {
            // todo
            var result = _unitOfWork.Repository<UserModel, int>()
                 .FirstOrDefaultAsync(s => s.RStatus == EnumRStatus.Active).Result;
            return result;
        }

        public async Task<List<UserDropdownDto>> GetUserSelectedList(int companyId)
        {
            var data = await _unitOfWork.Repository<UserModel, int>().FindByConditionAsync(s => s.RStatus == EnumRStatus.Active);
            return data.Select(s => new UserDropdownDto { Id = s.Id, FullName = s.FullName }).ToList();
        }

        public async Task<bool> SendInvitation(int userId)
        {
            var user = await _unitOfWork.Repository<UserModel, int>().GetByIdAsync(userId);
            if (user == null)
                throw new BadRequestException("Invalid Request");

            var invitationUrl = string.Concat(AppSettings.QMSApi.BaseUrl, $"/api/user/{user.Id}/request-accept");
            var result = await _emailNotificationService.SendInfitation(user, invitationUrl);
            return result;
        }

        public async Task<bool> AcceptRequest(int userId)
        {
            var user = await _unitOfWork.Repository<UserModel, int>().GetByIdAsync(userId);
            if (user == null)
                throw new BadRequestException("Invalid Request");

            user.IsActive = true;
            user.RStatus = EnumRStatus.Active;
            user.UpdatedDate = DateTime.UtcNow;
            user.UpdatedBy = userId;
            await _unitOfWork.Repository<UserModel, int>().UpdateAsync(user);
            return await _unitOfWork.CommitAsync() > 0;
        }
    }
}
