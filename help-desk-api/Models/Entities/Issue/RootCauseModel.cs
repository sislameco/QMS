using Models.Entities.Org;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities.Issue
{
    [Table("RootCause", Schema = "issue")]
    public class RootCauseModel : BaseEntity<int>
    {
        public int FKCompanyId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public int DisplayOrder { get; set; }
        [ForeignKey("FKCompanyId")]
        public CompanyModel Company { get; set; }
    }
}