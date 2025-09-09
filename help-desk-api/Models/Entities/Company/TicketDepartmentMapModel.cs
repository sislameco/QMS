using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities.Company
{
    [Table("TicketDepartmentMap", Schema = "company")]
    public class TicketDepartmentMapModel : BaseEntity<long>
    {
        public long TicketId { get; set; }
        public long DepartmentId { get; set; }
    }
}