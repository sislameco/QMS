using Models.Dto.UserManagement;
using Models.Entities.UserManagement;
using Models.Enum;
using Repository;
using Utils.LoginData;
namespace Services.UserManagement
{
    public interface IRoleService
    {
        Task CreateRole(RoleInputDto role);
        Task UpdateRole(RoleUpdateInputDto role);
        Task DeleteRole(int roleId);
    }
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserInfos _userInfos;
        public RoleService(IUnitOfWork unitOfWork, IUserInfos userInfos)
        {
            _unitOfWork = unitOfWork;
            _userInfos = userInfos;
        }

        public async Task CreateRole(RoleInputDto role)
        {
            var newRole = new RoleModel
            {
                Name = role.Name,
                Description = role.Description,
                HomeUrl = role.HomeUrl,
                CreatedBy = _userInfos.GetCurrentUserId(),
                CreatedDate = DateTime.UtcNow
            };
            await _unitOfWork.Repository<RoleModel, int>().AddAsync(newRole);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateRole(RoleUpdateInputDto role)
        {
            var updateRole = await _unitOfWork.Repository<RoleModel, int>().FirstOrDefaultAsync(s => s.Id == role.Id);
            if (updateRole == null)
                throw new Exception("Role not found");

            updateRole.Name = role.Name;
            updateRole.Description = role.Description;
            updateRole.HomeUrl = role.HomeUrl;
            updateRole.UpdatedBy = _userInfos.GetCurrentUserId();
            updateRole.UpdatedDate = DateTime.UtcNow;
            await _unitOfWork.Repository<RoleModel, int>().UpdateAsync(updateRole);
            //_unitOfWork.RoleRepository.Update(role);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteRole(int roleId)
        {
            var role = await _unitOfWork.Repository<RoleModel, int>().FirstOrDefaultAsync(s => s.Id == roleId);
            if (role != null)
            {
                role.RStatus = EnumRStatus.Deleted;
                role.DeletedBy = _userInfos.GetCurrentUserId();
                role.DeletedDate = DateTime.UtcNow;
                await _unitOfWork.Repository<RoleModel, int>().UpdateAsync(role);
                await _unitOfWork.CommitAsync();
            }
        }
    }
}