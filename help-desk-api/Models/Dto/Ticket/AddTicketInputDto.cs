using Models.Dto.Org;
using Models.Enum;

namespace Models.Dto.Ticket
{
    public enum EnumTicketField
    {

        // Full Page Mode
        Company = 1,
        TicketNumber = 2,
        Subject = 3,
        Description = 4,
        LinkingItem = 5,

        // Tab Page Mode
        Watchers = 6,
        Comments = 7,
        ChangeLog = 8,
        Files = 7,

        // Ticket Specification Fields
        TicketType = 2, 
        Assignee = 4,
        Departments = 3,
        RootCause = 5,
        Resolution = 6,
        OverDue = 9,

        // Project/Customer Details = 11
        Project = 12,
        Customer = 11,

        // Aditional Info
        SubForm = 10,  // Details





    }
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
        public List<SubFromInputDto> SubForm { get; set; } // get api 
        public int FkResolutionId { get; set; } // ddl
        public int FkRootCauseId { get; set; } // ddl

        // Screen 4
        public int FKAssignUser { get; set; } // default select as per ticket type // ddl
        public int[] FKDepartmentId { get; set; } // ddl
        public int[] Files { get; set; } // api
    }
    public class TicketBasicDetailOutputDto
    {
        public int Id { get; set; }
        public string TicketNumber { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public EnumTicketStatus Status { get; set; }
        public EnumPriority Priority { get; set; }
        public TicketCompanyViewDto Company { get; set; }
        //public List<ListTicketOutputDto> LinkingItems { get; set; }
    }
    public class TicketBasicDetailInputDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }

    public class TicketSpecificationOutputDto
    {
        public int FkTicketTypeId { get; set; }
        public int? RootCauseId { get; set; }
        public int? ResolutionId { get; set; }
        public int? AssigneeId { get; set; }
        public List<int> DepartmentIds { get; set; }
    }


    public class ListTicketOutputDto
    {
        public int Id { get; set; }
        public string TicketNumber { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
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
        public List<int> ChangeLog { get; set; }
        public DateTime OverDue { get; set; }
        public List<SubFromOutputDto> SubFrom { get; set; }
        public bool IsCustomer { get; set; }
        public ProjectViewDto Project { get; set; }
        public CustomerViewDto Customer { get; set; }
        public int LinkingItem { get; set; }
        public List<TicketCommentOutputDto> Comments { get; set; }
        public List<WatcherOutputDto> Watchers { get; set; }
    }
    public class TicketLinkingItemOutputDto
    {
        public int Id { get; set; }
        public string TicketNumber { get; set; }
        public string Subject { get; set; }
        public string URL { get; set; }
    }
    public class WatcherOutputDto
    {
        public int Id { get; set; }
        public int FkUserId { get; set; }
        public string UserName { get; set; }
    }
    public class TicketCommentOutputDto
    {
        public int Id { get; set; }
        public int FkUserId { get; set; }
        public string CommentText { get; set; }
        public DateTime CommentedOn { get; set; }
        public string CommentedBy { get; set; }
        public bool IsAuthorized { get; set; }
    }
    public class TicketFieldOutputDto
    {
        public int Id { get; set; }
        public int FkTicketTypeId { get; set; }
        public int FkCustomeFieldId { get; set; }
        public string Value { get; set; }
    }
    public class TicketWatchersOutputDto
    {
        public int Id { get; set; }
        public int FkUserId { get; set; }
        public string AddedBy { get; set; }
        public DateTime AddedOn { get; set; }
    }

    public class ProjectViewDto
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }
    }
    public class CustomerViewDto
    {
        public int Id { get; set; }
        public string CustomaryDetail { get; set; }
    }
    public class TicketCompanyViewDto
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
    }
    public class SubFromOutputDto
    {
        public int FieldId { get; set; }
        public string DisplayName { get; set; }
        public int DisplayOrder { get; set; }
        public EnumDataType DataType { get; set; }
        public int Key { get; set; }
        public string Value { get; set; }
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
