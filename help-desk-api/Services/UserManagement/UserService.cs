using Microsoft.EntityFrameworkCore;
using Models.Dto.GlobalDto;
using Models.Dto.UserManagement;
using Models.Entities.UserManagement;
using Models.Enum;
using Repository;
using Repository.Repo.UserManagement;

namespace Services.UserManagement
{
    public interface IUserService
    {
        Task<UserModel?> GetByIdAsync(int id);
        Task<List<UserOutPutDto>> GetAllAsync(UserFilterDto inputDto);
        Task AddAsync(UserModel user);
        Task UpdateAsync(UserModel user);
        Task DeleteAsync(int id);
        UserModel GetSystemUser();
        Task<List<UserDropdownDto>> GetUserSelectedList(int companyId);
        Task<bool> SendInvitation(int userId);
    }
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserMGtRepository _userMGtRepository;
        public UserService(IUnitOfWork unitOfWork, IUserMGtRepository userMGtRepository)
        {
            _unitOfWork = unitOfWork;
            _userMGtRepository = userMGtRepository;
        }

        public async Task<UserModel?> GetByIdAsync(int id)
            => await _unitOfWork.Repository<UserModel, int>().GetByIdAsync(id);

        public async Task<List<UserOutPutDto>> GetAllAsync(UserFilterDto inputDto)
        {
            var data = _userMGtRepository.GetUsers(inputDto);
            return await data.ToListAsync();
        }

        public async Task AddAsync(UserModel user)
        {
            await _unitOfWork.Repository<UserModel, int>().AddAsync(user);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateAsync(UserModel user)
        {
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

        public  async Task<List<UserDropdownDto>> GetUserSelectedList(int companyId)
        {
            var data = await _unitOfWork.Repository<UserModel,int>().FindByConditionAsync(s=> s.RStatus == EnumRStatus.Active);
            return data.Select(s=> new UserDropdownDto { Id = s.Id, FullName = s.FullName}).ToList();
        }

        public async Task<bool> SendInvitation(int userId)
        {
            // an emails sending logic will be here
            return true;
        }
    }
}
