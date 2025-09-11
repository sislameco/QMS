using Models.Entities.UserManagement;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IUserService
    {
        Task<UserModel?> GetByIdAsync(long id);
        Task<IEnumerable<UserModel>> GetAllAsync();
        Task AddAsync(UserModel user);
        Task UpdateAsync(UserModel user);
        Task DeleteAsync(long id);
    }
}
