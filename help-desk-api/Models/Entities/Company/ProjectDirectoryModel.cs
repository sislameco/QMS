using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities.Company
{
    [Table("ProjectDirectory", Schema = "company")]
    public class ProjectDirectoryModel : BaseEntity<long>
    {
        public string ProjectNumber { get; set; }
    }
}