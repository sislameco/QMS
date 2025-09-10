using Models.Entities.Org;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities.Issue
{
    [Table("Resolution", Schema = "issue")]
    public class ResolutionModel : BaseEntity<long>
    {
        public long FKCompanyId { get; set; }
        [ForeignKey("FKCompanyId")]
        public CompanyModel Company { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public int DisplayOrder { get; set; }
    }
}