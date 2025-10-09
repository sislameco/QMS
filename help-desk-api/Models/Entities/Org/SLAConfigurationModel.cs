
using Models.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities.Org
{
    [Table("SLAConfiguration", Schema = "Org")]
    public class SLAConfigurationModel : BaseEntity<int>
    {
        public EnumQMSType Type { get; set; }        public EnumPriority Priority { get; set; }
        public int FKCompanyId { get; set; }
        [ForeignKey("FKCompanyId")]
        public CompanyModel Company { get; set; }
        public EnumUnit Unit { get; set; }
        public int ResponseTime { get; set; }
        public int ResolutionTime { get; set; }
        public int EscalationTime { get; set; }
    }
}
