using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities.Company
{
    [Table("TicketType", Schema = "company")]
    public class TicketTypeModel : BaseEntity<long>
    {
        public string Name { get; set; }
        public ICollection<TicketModel> Tickets { get; set; }
    }
}