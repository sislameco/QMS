using Models.Dto.GlobalDto;
using Models.Dto.UserManagement;
using Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dto.Org
{
    public class DepartmentSettingInputDto:PagedInputDto
    {
        public List<int> UserIds { get; set; }
    }

    public class DepartmentSettingOutputDto : PagedInputDto
    {
       public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ManagerName { get; set; }
        public string ManagerEmail { get; set; }
        public EnumRStatus Status { get; set; }
        public int TotalUsers { get; set; }
        public string[] Moduls { get; set; }
    }
    public class DepartmentSetupOutputDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ManagerId { get; set; }
        public EnumRStatus Status { get; set; }
        public List<MenuAccessDto> menus { get; set; }
    }
    public class DepartmentUpdateDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int ManagerId { get; set; }
        public EnumRStatus Status { get; set; }
        public List<RoleSetWithMenuActoinDto> FKMenuActionIds { get; set; }
    }
}
