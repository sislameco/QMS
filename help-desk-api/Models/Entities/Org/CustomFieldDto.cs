using Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities.Org
{
    public class CustomFieldDto
    {
        public int FkTicketTypeId { get; set; }
        public string DisplayName { get; set; }
        public CustomFieldType DataType { get; set; }
        public string? OptionsJson { get; set; }
        public bool IsRequired { get; set; }
    }
}
