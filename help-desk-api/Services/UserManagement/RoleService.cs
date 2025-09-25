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
        Task CreateRole(RoleInputDto role);
        Task UpdateRole(RoleUpdateInputDto role);
        Task DeleteRole(int roleId);
        Task<MenuResourceDto> GetMenuAccess(int roleId);
        Task<bool> SetMenuPermission(int roleId, List<RoleSetWithMenuActoinDto> menus);
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

        public async Task<MenuResourceDto> GetMenuAccess(int roleId)
        {
            // Validate input
            if (roleId <= 0)
                throw new ArgumentException("Invalid roleId provided", nameof(roleId));

            // Run both queries in parallel for better performance
            var actionsTask = _unitOfWork.Repository<MenuActionModel, int>().GetAllAsync();
            var menusTask = _menuRepository.GetRolePermittedMenusAsync(roleId);

            await Task.WhenAll(actionsTask, menusTask);

            // Ensure results are not null
            var actions = actionsTask.Result ?? new List<MenuActionModel>();
            var menus = menusTask.Result ?? new List<MenuAccessDto>();

            // Map to DTO and return
            return new MenuResourceDto
            {
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
            var existingMappings = await _unitOfWork.Repository<MenuActionRoleMappingModel, long>()
                .FindByConditionAsync(s => s.FKRoleId == roleId && s.RStatus == EnumRStatus.Active);
            existingMappings.ToList();

            foreach(var menu in menus)
            {
                var existingMapping = existingMappings.FirstOrDefault(s => s.FKMenuActionMapId == menu.FKMenuActionId);
                if (existingMapping != null)
                {
                    existingMapping.IsAllowed = menu.IsAllowed;
                    existingMapping.UpdatedBy = _userInfos.GetCurrentUserId();
                    existingMapping.UpdatedDate = DateTime.UtcNow;
                    await _unitOfWork.Repository<MenuActionRoleMappingModel, long>().UpdateAsync(existingMapping);
                }
                if (existingMapping == null)
                {
                    var newMapping = new MenuActionRoleMappingModel
                    {
                        FKRoleId = roleId,
                        FKMenuActionMapId = menu.FKMenuActionId,
                        IsAllowed = menu.IsAllowed,
                        CreatedBy = _userInfos.GetCurrentUserId(),
                         CreatedDate = DateTime.UtcNow
                    };
                    await _unitOfWork.Repository<MenuActionRoleMappingModel, long>().AddAsync(newMapping);
                }
            }
            await _unitOfWork.CommitAsync();
            return true;
        }

    }
}