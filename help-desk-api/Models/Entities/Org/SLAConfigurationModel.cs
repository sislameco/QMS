using Models.Entities.UserManagement;
using Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities.Org
{
    [Table("SLAConfiguration", Schema = "Org")]
    public class SLAConfigurationModel : BaseEntity<int>
    {
        public QMSType Type { get; set; }
        public TicketPriority Priority { get; set; }
        public int FKCompanyId { get; set; }
        [ForeignKey("FKCompanyId")]
        public CompanyModel Company { get; set; }
        public EnumUnit Unit { get; set; }
        public int Value { get; set; }
    }
}
