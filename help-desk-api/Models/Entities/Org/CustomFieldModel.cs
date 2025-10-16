using Models.Entities;
using Models.Entities.Issue;
using Models.Entities.UserManagement;
using Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities.Org
{
    [Table("CustomFields", Schema = "Org")]
    public class CustomFieldModel : BaseEntity<int>
    {
        public int FkTicketTypeId { get; set; }
        public string DisplayName { get; set; }
        public EnumDataType DataType { get; set; } 
        public string[] DDLValue { get; set; }
        public bool IsRequired { get; set; }
        [ForeignKey("FkTicketTypeId")]
        public TicketTypeModel TicketType { get; set; }
        public string Description { get; set; }
        public bool IsMultiSelect { get; set; }
    }
    [Table("TicketCustomFields", Schema = "Org")]
    public class TicketCustomFieldValue : BaseEntity<int>
    {
        public int FkTicketId { get; set; }
        public int TicketTypeCustomFieldId { get; set; }
        public string Value { get; set; }
        [ForeignKey("TicketTypeCustomFieldId")]
        public CustomFieldModel CustomField { get; set; }
        [ForeignKey("FkTicketId")]
        public TicketModel Ticket { get; set; }
    }


}
