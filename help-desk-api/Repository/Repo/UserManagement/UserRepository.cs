using Models.Dto.UserManagement;
using Models.Entities.UserManagement;
using Repository.Db;
using System.Linq;

namespace Repository.Repo.UserManagement
{
    public interface IUserMGtRepository
    {
        IQueryable<UserOutPutDto> GetUsers(UserFilterDto inputDto);
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
            //        Departments = string.Join(", ",
            //g.First().user.UserDepartments.Select(d => d.Department.Name))
                };

            // Paging
            if (inputDto.PageNo > 0 && inputDto.ItemsPerPage > 0)
            {
                int skip = (inputDto.PageNo - 1) * inputDto.ItemsPerPage;
                query = query.Skip(skip).Take(inputDto.ItemsPerPage);
            }

            return query;
        }
    }
}
