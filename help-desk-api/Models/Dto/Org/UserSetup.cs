using Models.Dto.GlobalDto;
using Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dto.Org
{
    public class UserPaginationInputDto : PagedInputDto
    {
        public int DepartmentId { get; set; }
        public int RoleId { get; set; }

    }
    public class UserSetupOutputDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public int EmailAddress { get; set; }
        public string DepartmentName { get; set; }
        public string RoleName { get; set; }
        public int DepartmentId { get; set; }
        public int RoleId { get; set; }
    }
    public class UserSetupInputDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public int DepartmentId { get; set; }
        public int RoleId { get; set; }
        public EnumRStatus Status { get; set; }
    }

    public class HostUserInputDto : HostUserUpdateInputDto
    {
        public string UserName { get; set; } = string.Empty;
        public string PasswordHash { get; set; }
    }

    public class HostUserUpdateInputDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }
}
