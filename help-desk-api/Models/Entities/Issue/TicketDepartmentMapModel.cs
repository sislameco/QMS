using Models.Entities.Org;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities.Issue
{
    [Table("TicketDepartmentMap", Schema = "issue")]
    public class TicketDepartmentMapModel : BaseEntity<int>
    {
        public int FKTicketId { get; set; }
        public int FKDepartmentId { get; set; }

        [ForeignKey("FKTicketId")]
        public TicketModel Ticket { get; set; }

        [ForeignKey("FKDepartmentId")]
        public DepartmentModel Department { get; set; }
    }
}