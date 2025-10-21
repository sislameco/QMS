
using Models.Entities.Issue;
using Models.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities.Org
{
    [Table("SLAConfiguration", Schema = "Org")]
    public class SLAConfigurationModel : BaseEntity<int>
    {
        public int FKTicketTypeId { get; set; }       
        public int FKCompanyId { get; set; }
        [ForeignKey("FKCompanyId")]
        public  CompanyModel Company { get; set; }
        public EnumUnit Unit { get; set; }
        public int ResponseTime { get; set; }
        public int ResolutionTime { get; set; }
        public int EscalationTime { get; set; }
        [ForeignKey("FKTicketTypeId")]
        public TicketTypeModel TicketType { get; set; }
    }
}
