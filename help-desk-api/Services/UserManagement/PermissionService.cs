using Models.Dto.Menus;
using Models.Dto.UserManagement;
using Models.Entities.UserManagement;
using Repository;
using Repository.Repo.Permission;
using Utilities.Redis;
using Utils.Exceptions;
using Utils.LoginData;

namespace Services.UserManagement
{
    public interface IPermissionService
    {
        Task<bool> AssignRolesAsync(UserRoleAssignDto request);
        Task<List<UserRolePermissionOutputDto>> GetUserRolesAsync(long userId);
        Task<bool> RemoveUserRolesAsync(long userId, List<long> roleIds);
        Task<List<PerMenuDto>> GetUserMenus(int userId);
    }
    public class PermissionService : IPermissionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMenuRepository _menuRepository;
        private readonly  IUserInfos _user;
        public PermissionService(IUnitOfWork unitOfWork, IMenuRepository menuRepository, IUserInfos user)
        {
            _unitOfWork = unitOfWork;
            _menuRepository = menuRepository;
            _user = user;
        }

        public async Task<bool> AssignRolesAsync(UserRoleAssignDto request)
        {
            var user = await _unitOfWork.Repository<UserRoleModel, long>().FindByConditionAsync(s=> s.FKUserId == request.UserId);

            // Remove existing roles
            var removeRoles = await _unitOfWork.Repository<UserRoleModel, long>()
                .FindByConditionAsync(x => x.FKUserId == request.UserId && !request.RoleIds.Contains(x.FKRoleId));
            removeRoles = removeRoles.ToList();
            if (removeRoles.Any())
            {
                foreach (var role in removeRoles)
                {
                    if (role == null)
                        continue;
                    await _unitOfWork.Repository<UserRoleModel, long>().DeleteAsync(role.Id);
                }

            }

            // Add new roles
            foreach (var roleId in request.RoleIds)
            {
                await _unitOfWork.Repository<UserRoleModel, long>().AddAsync(new UserRoleModel
                {
                    FKUserId = request.UserId,
                    FKRoleId = roleId
                });
            }

            await _unitOfWork.CommitAsync();

            return true;
        }

        public async Task<List<UserRolePermissionOutputDto>> GetUserRolesAsync(long userId)
        {
            var roles = await _unitOfWork.Repository<UserRoleModel, long>()
                .FindByConditionAsync(x => x.FKUserId == userId);
            return new List<UserRolePermissionOutputDto>();
            //return new UserRolePermissionOutputDto
            //{
            //    UserId = userId,
            //    Roles = roles.ToList()
            //};
        }

        public async Task<bool> RemoveUserRolesAsync(long userId, List<long> roleIds)
        {
            var rolesToRemoves = await _unitOfWork.Repository<UserRoleModel, long>()
                .FindByConditionAsync(x => x.FKUserId == userId && roleIds.Contains(x.FKRoleId));

            rolesToRemoves = rolesToRemoves.ToList();

            foreach (var role in rolesToRemoves)
                await _unitOfWork.Repository<UserRoleModel, long>().DeleteAsync(role.Id);

            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<UserMenus> GetUserMenus()
        {
            int userId = _user.GetCurrentUserId();
            if (userId < 1)
                throw new BadRequestException("Invalid User Id!");

            UserMenus userMenus = AuthCacheUtil.GetPermittedUserResources(userId);

            if (userMenus != null)
                return userMenus;

            // get permitted menu, user info and powerBi data
           // userMenus = await GetUserResource(userId, templateId);

            // Set new Cache
            AuthCacheUtil.SetPermittedUserResources(userId, userMenus);

            return userMenus;
        }
        public async Task<List<PerMenuDto>> GetUserMenus(int userId)
        {
            var menus = await _menuRepository.GetUserPermittedMenusAsync(userId);
            return menus;
        }
    }

}
