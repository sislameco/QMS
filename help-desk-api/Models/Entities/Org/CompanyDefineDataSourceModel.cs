using Models.Entities.Issue;
using Models.Enum;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities.Org
{
    [Table("CompanyDefineDataSources", Schema = "Org")]
    public class CompanyDefineDataSourceModel:BaseEntity<int>
    {
        public int FkCompanyId { get; set; }
        public string Source { get; set; }
        public bool IsSync { get; set; }
        public string JsonData { get; set; }
        [ForeignKey("FkCompanyId")]
        public CompanyModel Company { get; set; }
        public EnumDataSource DataSourceType { get; set; }
    }
}
