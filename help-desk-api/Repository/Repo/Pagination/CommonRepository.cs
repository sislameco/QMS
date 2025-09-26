using Microsoft.EntityFrameworkCore;
using Models.Dto.Pagination;
using Models.Dto.UserManagement;
using Models.Enum;
using Repository.Db;


namespace Repository.Repo.Pagination
{
    public interface ICommonRepository
    {
        Task<PaginationResponse<RoleWithUsersDto>> GetRolesWithUsersAsync();
    }
    public class CommonRepository : ICommonRepository
    {
        private readonly HelpDbContext _dbContext;

        public CommonRepository(HelpDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<PaginationResponse<RoleWithUsersDto>> GetRolesWithUsersAsync()
        {
            var result = await (from role in _dbContext.Roles
                                where role.RStatus == EnumRStatus.Active
                                select new RoleWithUsersDto
                                {
                                    RoleId = role.Id,
                                    RoleName = role.Name,
                                    Type = role.IsSystemGenerated ? "System" : "Custom",
                                    Description = role.Description,
                                    Users = (from ur in _dbContext.UserRoles
                                             join user in _dbContext.Users
                                             on ur.FKUserId equals user.Id
                                             where ur.FKRoleId == role.Id
                                                 && ur.RStatus == EnumRStatus.Active
                                                 && user.RStatus == EnumRStatus.Active
                                             select user == null ? "" : string.Concat(user.FirstName, " ", user.LastName)).FirstOrDefault()
                                }).ToListAsync();
            return new PaginationResponse<RoleWithUsersDto>
            {
                Items = result,
                Total = result.Count
            };
        }
    }
}
