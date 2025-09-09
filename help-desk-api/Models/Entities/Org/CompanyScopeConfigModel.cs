using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Models.Entities;

namespace Models.Entities.Org
{
    [Table("CompanyScopeConfig", Schema = "Org")]
    public class CompanyScopeConfigModel : BaseEntity<long>
    {
        public long CompanyId { get; set; }
        public CompanyModel Company { get; set; }
        public string PrefixTicket { get; set; }
        public string PrefixComplaint { get; set; }
        public string PrefixCAPA { get; set; }
    }
}