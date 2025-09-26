using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dto.UserManagement
{
    public class RoleWithUsersDto
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string Users { get; set; }
    }
    public class UserRoleAssignDto
    {
        public int UserId { get; set; }
        public List<int> RoleIds { get; set; } = new();
    }
    public class UserRolePermissionOutputDto
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public int RoleName { get; set; }
        public int? CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string UserName { get; set; }
    }
}
