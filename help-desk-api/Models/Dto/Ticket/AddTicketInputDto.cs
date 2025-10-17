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
        public int? FKCustomerId { get; set; } // ddl
        public int? FKProjectId { get; set; } // ddl
        // Screen 3 
        public int FkTicketTypeId { get; set; } // ddl
        public List<SubFromInputDto> SubFrom { get; set; } // get api 
        public int FkRelocationId { get; set; } // ddl
        public int FkRootCauseId { get; set; } // ddl

        // Screen 4
        public int FKAssignUser { get; set; } // default select as per ticket type // ddl
        public int[] FKDepartmentId { get; set; } // ddl
        public int[] Files { get; set; } // api
    }
}
