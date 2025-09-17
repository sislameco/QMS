using Models.Entities.UserManagement;
using Repository;
using Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<UserModel?> GetByIdAsync(long id)
            => await _unitOfWork.Repository<UserModel, long>().GetByIdAsync(id);

        public async Task<IEnumerable<UserModel>> GetAllAsync()
            => await _unitOfWork.Repository<UserModel, long>().GetAllAsync();

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

        public async Task<UserModel?> ValidateCredentialsAsync(string email, string password)
        {
            var user = (await _unitOfWork.Repository<UserModel, long>()
                .FindByConditionAsync(u => u.Email == email && u.IsActive))
                .FirstOrDefault();

            if (user == null) return null;

            // Use a secure password hash comparison in production!
            if (user.PasswordHash == password) // Replace with hash check
                return user;

            return null;
        }
    }
}
