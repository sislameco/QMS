using Models.Dto.Org;
using Models.Dto.UserManagement;
using Models.Entities.UserManagement;
using Models.Enum;
using Repository.Db;
using System.Linq;
using Utils.Exceptions;

namespace Repository.Repo.UserManagement
{
    public interface IUserMGtRepository
    {
        IQueryable<UserOutPutDto> GetUsers(UserFilterDto inputDto);
        List<UserSetupOutputDto> GetTenentUser(int companyId, UserPaginationInputDto inputDto);
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
            var users = _context.Users.AsQueryable();
            var roles = _context.Roles.AsQueryable();

            // Filtering by Roles
            if (inputDto.RoleIds != null && inputDto.RoleIds.Count > 0)
                users = users.Where(u => u.UserRoles.Any(r => inputDto.RoleIds.Contains(r.FKRoleId)));

            // Filtering by Departments
            //if (inputDto.DepartmentIds != null && inputDto.DepartmentIds.Any())
            //    users = users.Where(u => u.UserDepartments.Any(d => inputDto.DepartmentIds.Contains(d.FKDepartmentId)));

            // Filtering by SearchText
            if (!string.IsNullOrWhiteSpace(inputDto.SearchText))
            {
                users = users.Where(u =>
                    u.FullName.Contains(inputDto.SearchText) ||
                    u.Email.Contains(inputDto.SearchText) ||
                    u.UserName.Contains(inputDto.SearchText));
            }

            // Filtering by Status
            users = users.Where(u => u.RStatus == inputDto.Status);

            var query =
                from user in _context.Users
                join userRole in _context.UserRoles on user.Id equals userRole.FKUserId
                join role in _context.Roles on userRole.FKRoleId equals role.Id
                group new { user, role } by user.Id into g
                select new UserOutPutDto
                {
                    FullName = g.First().user.FullName,
                    Email = g.First().user.Email,
                    Phone = g.First().user.Phone ?? g.First().user.UserName,
                    Status = g.First().user.RStatus,
                    // 🔹 Roles concatenated
                    Roles = string.Join(", ", g.Select(x => x.role.Name).Distinct()),
                    // 🔹 Departments (optional, same pattern)
                    // Departments = string.Join(", ",
                    // g.First().user.UserDepartments.Select(d => d.Department.Name))
                };

            // Paging
            if (inputDto.PageNo > 0 && inputDto.ItemsPerPage > 0)
            {
                int skip = (inputDto.PageNo - 1) * inputDto.ItemsPerPage;
                query = query.Skip(skip).Take(inputDto.ItemsPerPage);
            }

            return query;
        }

        public List<UserSetupOutputDto> GetTenentUser(int companyId, UserPaginationInputDto inputDto)
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
                 where (user.RStatus == inputDto.Status
                        && userRole.RStatus == EnumRStatus.Active
                        && role.RStatus == EnumRStatus.Active
                        && userDept.RStatus == EnumRStatus.Active)
                        && user.FkCompanyId == companyId
                    && (inputDto.RoleId == 0 || role.Id == inputDto.RoleId)
                    && (inputDto.DepartmentId == 0 || userDept.Id == inputDto.DepartmentId)
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
                     RoleName = g.Select(x => x.role.Name).FirstOrDefault()
                 }).Skip(skip).Take(inputDto.ItemsPerPage).ToList();
            return users;
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
                     RoleName = g.Select(x => x.role.Name).FirstOrDefault()
                 }).FirstOrDefault();
            return tenentUser == null ? throw new BadRequestException("User Not Found") : tenentUser;
        }
    }
}

