using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities.Company
{
    [Table("RootCause", Schema = "company")]
    public class RootCauseModel : BaseEntity<long>
    {
        public string Name { get; set; }
    }
}