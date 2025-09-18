using Models.Dto.UserManagement;
using Models.Entities.UserManagement;
using Repository;

namespace Services.UserManagement
{
    public interface IPermissionService
    {
        Task<bool> AssignRolesAsync(UserRoleAssignDto request);
        Task<List<UserRolePermissionOutputDto>> GetUserRolesAsync(long userId);
        Task<bool> RemoveUserRolesAsync(long userId, List<long> roleIds);
    }
    public class PermissionService : IPermissionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PermissionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
    }

}
