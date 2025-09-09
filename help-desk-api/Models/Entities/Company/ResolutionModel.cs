using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities.Company
{
    [Table("Resolution", Schema = "company")]
    public class ResolutionModel : BaseEntity<long>
    {
        public string Name { get; set; }
    }
}