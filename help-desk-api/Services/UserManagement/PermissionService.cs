using Models.Dto.GlobalDto;
using Models.Dto.Menus;
using Models.Dto.UserManagement;
using Models.Entities.UserManagement;
using Models.Enum;
using Repository;
using Repository.Repo.Permission;
using StackExchange.Redis;
using Utilities.Redis;
using Utils.Exceptions;
using Utils.LoginData;

namespace Services.UserManagement
{
    public interface IPermissionService
    {
        Task<bool> AssignRolesAsync(UserRoleAssignDto request);
        Task<List<UserRolePermissionOutputDto>> GetUserRolesAsync(int userId);
        Task<bool> RemoveUserRolesAsync(int userId, List<int> roleIds);
        Task<List<PerMenuDto>> GetLoginUserMenus();
        Task<List<IntegratedMenuOutputDto>> GetPermittedModules();
        Task<MenuResourceDto> GetMenuAccess(int roleId);
        Task<bool> SetMenuPermission(int roleId, List<RoleSetWithMenuActoinDto> menus);
        Task<List<DropdownOutputDto<int,string>>> GetModules();
    }
    public class PermissionService : IPermissionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMenuRepository _menuRepository;
        private readonly  IUserInfos _user;
        private readonly IUserInfos _userInfos;
        public PermissionService(IUnitOfWork unitOfWork, IMenuRepository menuRepository, IUserInfos user, IUserInfos userInfos)
        {
            _unitOfWork = unitOfWork;
            _menuRepository = menuRepository;
            _user = user;
            _userInfos = userInfos;
        }

        public async Task<bool> AssignRolesAsync(UserRoleAssignDto request)
        {
            var user = await _unitOfWork.Repository<UserRoleModel, int>().FindByConditionAsync(s=> s.FKUserId == request.UserId);

            // Remove existing roles
            var removeRoles = await _unitOfWork.Repository<UserRoleModel, int>()
                .FindByConditionAsync(x => x.FKUserId == request.UserId && !request.RoleIds.Contains(x.FKRoleId));
            removeRoles = removeRoles.ToList();
            if (removeRoles.Any())
            {
                foreach (var role in removeRoles)
                {
                    if (role == null)
                        continue;
                    await _unitOfWork.Repository<UserRoleModel, int>().DeleteAsync(role.Id);
                }

            }

            // Add new roles
            foreach (var roleId in request.RoleIds)
            {
                await _unitOfWork.Repository<UserRoleModel, int>().AddAsync(new UserRoleModel
                {
                    FKUserId = request.UserId,
                    FKRoleId = roleId
                });
            }

            await _unitOfWork.CommitAsync();

            return true;
        }

        public async Task<List<UserRolePermissionOutputDto>> GetUserRolesAsync(int userId)
        {
            var roles = await _unitOfWork.Repository<UserRoleModel, int>()
                .FindByConditionAsync(x => x.FKUserId == userId);
            return new List<UserRolePermissionOutputDto>();
            //return new UserRolePermissionOutputDto
            //{
            //    UserId = userId,
            //    Roles = roles.ToList()
            //};
        }

        public async Task<bool> RemoveUserRolesAsync(int userId, List<int> roleIds)
        {
            var rolesToRemoves = await _unitOfWork.Repository<UserRoleModel, int>()
                .FindByConditionAsync(x => x.FKUserId == userId && roleIds.Contains(x.FKRoleId));

            rolesToRemoves = rolesToRemoves.ToList();

            foreach (var role in rolesToRemoves)
                await _unitOfWork.Repository<UserRoleModel, int>().DeleteAsync(role.Id);

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
        public async Task<List<PerMenuDto>> GetLoginUserMenus()
        {
            var userId = _user.GetCurrentUserId();
            var menus = await _menuRepository.GetUserPermittedMenusAsync(userId);
            return menus;
        }
        public async Task<List<IntegratedMenuOutputDto>> GetPermittedModules()
        {
            var userId = _user.GetCurrentUserId();
            var menus = await _menuRepository.GetUserPermittedModuleAsync(userId);
            return menus;
        }
        public async Task<MenuResourceDto> GetMenuAccess(int roleId)
        {
            // Validate input
            if (roleId < 0)
                throw new ArgumentException("Invalid roleId provided", nameof(roleId));

            // Run both queries in parallel for better performance

            var role = await _unitOfWork.Repository<RoleModel, int>().FirstOrDefaultAsync(s => s.Id == roleId);
            var actions = await _unitOfWork.Repository<MenuActionModel, int>().GetAllAsync();
            var menus = await _menuRepository.GetRolePermittedMenusAsync(roleId);
            // Map to DTO and return
            return new MenuResourceDto
            {
                Role = role == null ? new RoleDetail() : new RoleDetail { Name = role.Name, Description = role.Description },
                Actions = actions
                    .Select(s => new ManuActionDto
                    {
                        Id = s.Id,
                        HttpVerb = s.HttpVerb
                    })
                    .ToList(),

                Menus = menus
            };
        }
        public async Task<bool> SetMenuPermission(int roleId, List<RoleSetWithMenuActoinDto> menus)
        {
            // Remove existing permissions for the role
            var existingMappings = await _unitOfWork.Repository<MenuActionRoleMappingModel, int>()
                .FindByConditionAsync(s => s.FKRoleId == roleId && s.RStatus == EnumRStatus.Active);
            existingMappings.ToList();

            foreach (var menu in menus)
            {
                var existingMapping = existingMappings.FirstOrDefault(s => s.FKMenuActionMapId == menu.FkMenuActionMapId);
                if (existingMapping != null)
                {
                    existingMapping.IsAllowed = menu.IsAllowed;
                    existingMapping.UpdatedBy = _userInfos.GetCurrentUserId();
                    existingMapping.UpdatedDate = DateTime.UtcNow;
                    await _unitOfWork.Repository<MenuActionRoleMappingModel, int>().UpdateAsync(existingMapping);
                }
                if (existingMapping == null)
                {
                    var newMapping = new MenuActionRoleMappingModel
                    {
                        FKRoleId = roleId,
                        FKMenuActionMapId = menu.FkMenuActionMapId,
                        IsAllowed = menu.IsAllowed,
                        CreatedBy = _userInfos.GetCurrentUserId(),
                        CreatedDate = DateTime.UtcNow
                    };
                    await _unitOfWork.Repository<MenuActionRoleMappingModel, int>().AddAsync(newMapping);
                }
            }
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<List<DropdownOutputDto<int, string>>> GetModules()
        {
            return _unitOfWork.Repository<MenuModel, int>()
         .FindByConditionSelected(s => s.IsModule == true && s.RStatus == EnumRStatus.Active, s => new DropdownOutputDto<int, string> { Id = s.Id, Name = s.Name }).ToList();
           
        }
    }

}
