using Models.Entities.Org;
using Models.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities.Setup
{
    [Table("CompanyDefineRootResolutions", Schema = "setup")]
    public class CompanyDefineRootResolutionModel : BaseEntity<int>
    {
        public int FKCompanyId { get; set; }
        [ForeignKey("FKCompanyId")]
        public CompanyModel Company { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int DisplayOrder { get; set; }
        public EnumRootResolutionType Type { get; set; }
    }
}