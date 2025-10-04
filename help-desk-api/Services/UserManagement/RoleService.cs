using Models.Dto.Pagination;
using Models.Dto.UserManagement;
using Models.Entities.UserManagement;
using Models.Enum;
using Repository;
using Repository.Repo.Permission;
using Utils.LoginData;
namespace Services.UserManagement
{
    public interface IRoleService
    {
        Task<PaginationResponse<RoleWithUsersDto>> GetRolesWithUsersAsync();
        Task<RoleDetail?> GetRoleByIdAsync(int roleId);
        Task CreateRole(RoleInputDto role);
        Task UpdateRole(int roleId, RoleInputDto role);
        Task DeleteRole(int roleId);
    }
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserInfos _userInfos;
        private readonly IMenuRepository _menuRepository;
        public RoleService(IUnitOfWork unitOfWork, IUserInfos userInfos, IMenuRepository menuRepository)
        {
            _unitOfWork = unitOfWork;
            _userInfos = userInfos;
            _menuRepository = menuRepository;
        }
        public async Task<PaginationResponse<RoleWithUsersDto>> GetRolesWithUsersAsync()
        {
            return await _unitOfWork.CommonRepository.GetRolesWithUsersAsync();
        }
        public async Task<RoleDetail?> GetRoleByIdAsync(int roleId)
        {
            var role = await _unitOfWork.Repository<RoleModel, int>().GetByIdAsync(roleId);
            if (role == null || role.RStatus == EnumRStatus.Deleted)
                return null;

            return new RoleDetail
            {
                Name = role.Name,
                Description = role.Description,
                Menus = await _menuRepository.GetRolePermittedMenusAsync(roleId)
            };
        }
        public async Task CreateRole(RoleInputDto role)
        {
            var newRole = new RoleModel
            {
                Name = role.Name,
                Description = role.Description,
                IsSystemGenerated = false,
                CreatedBy = _userInfos.GetCurrentUserId(),
                CreatedDate = DateTime.UtcNow
            };
            await _unitOfWork.Repository<RoleModel, int>().AddAsync(newRole);
            await _unitOfWork.CommitAsync();

            await SetMenuPermission(newRole.Id, role.FKMenuActionIds);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateRole(int roleId, RoleInputDto role)
        {
            var updateRole = await _unitOfWork.Repository<RoleModel, int>().FirstOrDefaultAsync(s => s.Id == roleId);
            if (updateRole == null)
                throw new Exception("Role not found");

            updateRole.Name = role.Name;
            updateRole.Description = role.Description;
            updateRole.UpdatedBy = _userInfos.GetCurrentUserId();
            updateRole.UpdatedDate = DateTime.UtcNow;
            await _unitOfWork.Repository<RoleModel, int>().UpdateAsync(updateRole);
            await SetMenuPermission(roleId, role.FKMenuActionIds);
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
        private async Task<bool> SetMenuPermission(int roleId, List<RoleSetWithMenuActoinDto> menus)
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
            return true;
        }

    }
}