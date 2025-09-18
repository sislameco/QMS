using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dto.UserManagement
{
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
