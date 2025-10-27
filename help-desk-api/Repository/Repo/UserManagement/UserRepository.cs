using Models.Dto.Org;
using Models.Dto.Pagination;
using Models.Dto.UserManagement;
using Models.Enum;
using Repository.Db;
using Utils.Exceptions;

namespace Repository.Repo.UserManagement
{
    public interface IUserMGtRepository
    {
        IQueryable<UserOutPutDto> GetUsers(UserFilterDto inputDto);
        Task<PaginationResponse<UserSetupOutputDto>> GetTenentUser(int companyId, UserPaginationInputDto inputDto);
        UserSetupOutputDto GetTenentUserById(int userId);
    }

    public class UserMGtRepository : IUserMGtRepository
    {
        private readonly HelpDbContext _context;

        public UserMGtRepository(HelpDbContext context)
        {
            _context = context;
        }

        public IQueryable<UserOutPutDto> GetUsers(UserFilterDto inputDto)
        {
            var query =
                from user in _context.Users
                join userRole in _context.UserRoles on user.Id equals userRole.FKUserId
                join role in _context.Roles on userRole.FKRoleId equals role.Id
                where user.RStatus == EnumRStatus.Active && user.FkCompanyId == null
                group new { user, role } by user.Id into g
                select new UserOutPutDto
                {
                    Id = g.Key,
                    FullName = g.First().user.FullName,
                    Email = g.First().user.Email,
                    Phone = g.First().user.Phone ?? "",
                    Status = g.First().user.RStatus,
                    Roles = string.Join(", ", g.Select(x => x.role.Name).Distinct()),
                    RoleId = g.First().role.Id,
                    FirstName = g.First().user.FirstName,
                    Lastname = g.First().user.LastName,
                     UserName = g.First().user.UserName

                };

            // Paging
            if (inputDto.PageNo > 0 && inputDto.ItemsPerPage > 0)
            {
                int skip = (inputDto.PageNo - 1) * inputDto.ItemsPerPage;
                query = query.Skip(skip).Take(inputDto.ItemsPerPage);
            }

            return query;
        }

        public async Task<PaginationResponse<UserSetupOutputDto>> GetTenentUser(int companyId, UserPaginationInputDto inputDto)
        {
            int skip = (inputDto.PageNo - 1) * inputDto.ItemsPerPage;

            var users =
                (from user in _context.Users
                 join userRole in _context.UserRoles on user.Id equals userRole.FKUserId into ur
                 from userRole in ur.DefaultIfEmpty()
                 join role in _context.Roles on userRole.FKRoleId equals role.Id into r
                 from role in r.DefaultIfEmpty()
                 join userDepartment in _context.Departments on user.FkDepartmentId equals userDepartment.Id into ud
                 from userDept in ud.DefaultIfEmpty()
                 where (user.RStatus == inputDto.Status) && (userRole.RStatus == EnumRStatus.Active || userRole == null)
                        && ( role == null || role.RStatus == EnumRStatus.Active)
                        && (userDept == null || userDept.RStatus == EnumRStatus.Active)
                        && user.FkCompanyId == companyId
                    && (inputDto.RoleId == 0 || role.Id == inputDto.RoleId)
                  //  && (inputDto.DepartmentIds == 0 || userDept.Id == inputDto.DepartmentIds)
                    && (string.IsNullOrEmpty(inputDto.SearchText) || user.UserName.Contains(inputDto.SearchText) || user.Email.Contains(inputDto.SearchText) || user.Phone.Contains(inputDto.SearchText))
                 group new { user, role, userDept } by new
                 {
                     user.Id,
                 } into g
                 select new UserSetupOutputDto
                 {
                     Id = g.Key.Id,
                     DepartmentName = g.Select(s => s.userDept.Name).FirstOrDefault(),
                     DepartmentId = g.Select(s => s.user.FkDepartmentId ?? 0).FirstOrDefault(),
                     UserName = g.Select(s => s.user.UserName).FirstOrDefault(),
                     Email = g.Select(s => s.user.Email).FirstOrDefault(),
                     PhoneNumber = g.Select(s => s.user.Phone).FirstOrDefault(),
                     RoleId = g.Select(s => s.role.Id).FirstOrDefault(),
                     RoleName = g.Select(x => x.role.Name).FirstOrDefault(),
                     IsAdmin = g.Select(s => s.user.IsTenantAdmin).FirstOrDefault()
                 }).Skip(skip).Take(inputDto.ItemsPerPage).ToList();

            return new PaginationResponse<UserSetupOutputDto>
            {
                Items = users,
                Page = inputDto.PageNo,
                PageSize = inputDto.ItemsPerPage,
                Total = users.Count
            };
        }

        public UserSetupOutputDto GetTenentUserById(int userId)
        {
            var tenentUser =
                (from user in _context.Users
                 join userRole in _context.UserRoles on user.Id equals userRole.FKUserId into ur
                 from userRole in ur.DefaultIfEmpty()
                 join role in _context.Roles on userRole.FKRoleId equals role.Id into r
                 from role in r.DefaultIfEmpty()
                 join userDepartment in _context.Departments on user.FkDepartmentId equals userDepartment.Id into ud
                 from userDept in ud.DefaultIfEmpty()
                 where user.Id == userId
                 group new { user, role, userDept } by new { user.Id } into g
                 select new UserSetupOutputDto
                 {
                     Id = g.Key.Id,
                     DepartmentName = g.Select(s => s.userDept.Name).FirstOrDefault(),
                     DepartmentId = g.Select(s => s.user.FkDepartmentId ?? 0).FirstOrDefault(),
                     UserName = g.Select(s => s.user.UserName).FirstOrDefault(),
                     Email = g.Select(s => s.user.Email).FirstOrDefault(),
                     PhoneNumber = g.Select(s => s.user.Phone).FirstOrDefault(),
                     RoleId = g.Select(s => s.role.Id).FirstOrDefault(),
                     RoleName = g.Select(x => x.role.Name).FirstOrDefault(),
                     IsAdmin = g.Select(s => s.user.IsTenantAdmin).FirstOrDefault()
                 }).FirstOrDefault();
            return tenentUser == null ? throw new BadRequestException("User Not Found") : tenentUser;
        }
    }
}

