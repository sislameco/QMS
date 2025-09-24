using Microsoft.EntityFrameworkCore;
using Models.Dto.UserManagement;
using Models.Entities.UserManagement;
using Models.Enum;
using Repository;
using Repository.Repo.UserManagement;

namespace Services.UserManagement
{
    public interface IUserService
    {
        Task<UserModel?> GetByIdAsync(long id);
        Task<List<UserOutPutDto>> GetAllAsync(UserFilterDto inputDto);
        Task AddAsync(UserModel user);
        Task UpdateAsync(UserModel user);
        Task DeleteAsync(long id);
        UserModel GetSystemUser();
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

        public async Task<UserModel?> GetByIdAsync(long id)
            => await _unitOfWork.Repository<UserModel, long>().GetByIdAsync(id);

        public async Task<List<UserOutPutDto>> GetAllAsync(UserFilterDto inputDto)
        {
            var data = _userMGtRepository.GetUsers(inputDto);
            return await data.ToListAsync();
        }

        public async Task AddAsync(UserModel user)
        {
            await _unitOfWork.Repository<UserModel, long>().AddAsync(user);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateAsync(UserModel user)
        {
            await _unitOfWork.Repository<UserModel, long>().UpdateAsync(user);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteAsync(long id)
        {
            await _unitOfWork.Repository<UserModel, long>().DeleteAsync(id);
            await _unitOfWork.CommitAsync();
        }
        public UserModel GetSystemUser()
        {
            // todo
            var result = _unitOfWork.Repository<UserModel, long>()
                 .FirstOrDefaultAsync(s => s.RStatus == EnumRStatus.Active).Result;
            return result;
        }
    }
}
