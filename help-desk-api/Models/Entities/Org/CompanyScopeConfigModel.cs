using Models.Entities;
using Models.Entities.Issue;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities.Org
{
    [Table("CompanyScopeConfig", Schema = "Org")]
    public class CompanyScopeConfigModel : BaseEntity<long>
    {
        public long FKCompanyId { get; set; }
        public string PrefixTicket { get; set; }
        public string PrefixComplaint { get; set; }
        public string PrefixCAPA { get; set; }
        [ForeignKey("FKCompanyId")]
        public CompanyModel Company { get; set; }
    }
}