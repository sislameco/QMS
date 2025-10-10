using Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dto.Org
{
    public class TicketTypeCommonDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int? FKAssignedUserId { get; set; }
        public EnumPriority Priority { get; set; }
        public bool IsEnabled { get; set; }
        public int[] FKDepartmentIds { get; set; }
        public int FKCompanyId { get; set; }
    }
    public class TicketTypeOutputDto: TicketTypeCommonDto
    {
        public int Id { get; set; }
        public string[] DepartmentNames { get; set; }
    }
    public class TicketTypeInputDto: TicketTypeCommonDto
    {
    }
}
