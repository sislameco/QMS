using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dto.Intregation
{
   public class DepartmentOutputDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class UserWithEmployeeDto
    {
        public int UserId { get; set; }
        public int FkReportingManagerId { get; set; }
        public int EmployeeId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public int FkDepartmentId { get; set; }
    }
}
