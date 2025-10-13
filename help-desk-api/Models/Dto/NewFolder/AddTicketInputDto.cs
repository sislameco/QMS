using Models.Dto.Org;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dto.NewFolder
{
    public class AddTicketInputDto
    {
        // Screen 1
        public int FkCompanyId { get; set; }
        public int Subject { get; set; }
        // Screen 2
        public bool IsCustomer { get; set; }
        public int? FkCustomerId { get; set; }
        public int? FkProjectId { get; set; }
        // Screen 3
        public List<SubFromInputDto> SubFrom { get; set; }
        // Screen 4
        public int[] FkDepartmentId { get; set; }
        public string[] Files { get; set; }


    }
}
