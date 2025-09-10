using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities.Issue
{
    [Table("TicketDepartmentMap", Schema = "issue")]
    public class TicketDepartmentMapModel : BaseEntity<long>
    {
        public long TicketId { get; set; }
        public long DepartmentId { get; set; }
    }
}