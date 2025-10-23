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
        public string Subject { get; set; }
        public string Description { get; set; }
        // Screen 2
        public bool IsCustomer { get; set; }
        public int? FKCustomerId { get; set; } // ddl
        public int? FKProjectId { get; set; } // ddl
        // Screen 3 
        public int FkTicketTypeId { get; set; } // ddl
        public List<SubFromInputDto> SubFrom { get; set; } // get api 
        public int FkResolutionId { get; set; } // ddl
        public int FkRootCauseId { get; set; } // ddl

        // Screen 4
        public int FKAssignUser { get; set; } // default select as per ticket type // ddl
        public int[] FKDepartmentId { get; set; } // ddl
        public int[] Files { get; set; } // api
    }


    public class TicketViewOutputDto
    {
        public int Id { get; set; }
        public string TicketNumber { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public TicketCompanyViewDto Company { get; set; }
        public TicketTicketTypeViewDto TicketTypeView { get; set; }
        public List<TicketDepartmentViewDto> Departments { get; set; }
        public TicketAssignee Asignee { get; set; }
        public TicketSingleView RootCause { get; set; }
        public TicketSingleView Resolution { get; set; }
        public List<FileDto> Files { get; set; }
    }
    public class TicketCompanyViewDto
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
    }
    public class TicketTicketTypeViewDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
    public class TicketCustomerViewDto
    {
        public int Id { get; set; }
        public int FkCustomerId { get; set; }
        public string Name { get; set; }
    }
    public class TicketSingleView
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
    public class TicketProjectCustomerViewDto
    {
        public int Id { get; set; }
        public int FkProjectId { get; set; }
        public string Name { get; set; }
    }
    public class TicketAssignee
    {
        public int Id { get; set; }
        public int FkUserId { get; set; }
        public string Name { get; set; }
    }
    public class FileDto
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string AddedBy { get; set; }
        public string AddedOn { get; set; }
    }
    public class TicketDepartmentViewDto
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentId { get; set; }
        public string AddedBy { get; set; }
        public string AddedOn { get; set; }
    }

    public class TicketSubFromPutputDto
    {
        public int FkTicketTypeId { get; set; }

    }
}
