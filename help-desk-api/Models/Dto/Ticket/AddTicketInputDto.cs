using Models.Dto.Org;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dto.Ticket
{
    public class AddTicketInputDto
    {
        // Screen 1
        public int FKCompanyId { get; set; }
        public int Subject { get; set; }
        public string Description { get; set; }
        // Screen 2
        public bool IsCustomer { get; set; }
        public int? FKCustomerId { get; set; }
        public int? FKProjectId { get; set; }
        // Screen 3 
        public int FkTicketTypeId { get; set; }
        public List<SubFromInputDto> SubFrom { get; set; }
        public int FkRelocationId { get; set; }
        public int FkRootCauseId { get; set; }

        // Screen 4
        public int FKAssignUser { get; set; } // default select as per ticket type
        public int[] FKDepartmentId { get; set; }
        public int[] Files { get; set; }


    }
}
