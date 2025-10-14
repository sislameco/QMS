using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities.Issue
{
    [Table("TicketCustomerMaps", Schema = "issue")]
    public class TicketCustomerMapModel : BaseEntity<int>
    {
        public int FKTicketId { get; set; }
        public int FkCustomerId { get; set; }

        [ForeignKey("FKTicketId")]
        public TicketModel Ticket { get; set; }

        [ForeignKey("FkCustomerId")]
        public CompanyCustomerModel Project { get; set; }
    }
    [Table("TicketProjectMaps", Schema = "issue")]
    public class TicketProjectMapModel : BaseEntity<int>
    {
        public int FKTicketId { get; set; }
        public int FkProjectId { get; set; }

        [ForeignKey("FKTicketId")]
        public TicketModel Ticket { get; set; }

        [ForeignKey("FkProjectId")]
        public CompanyProjectModel Project { get; set; }
    }
}