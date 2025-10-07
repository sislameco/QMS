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
    public class QMSProjectDetailDto
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectNumber { get; set; }
        public string Address { get; set; }
    }
    public class CustomerDetailsOutPutDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }

    }
}
