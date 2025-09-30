using Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dto.Org
{
    public class SLAInputDto
    {
        public QMSType Type { get; set; }
        public TicketPriority Priority { get; set; }
        public int FKCompanyId { get; set; }
        public EnumUnit Unit { get; set; }
        public int Value { get; set; }
    }
    public class SLAOutputDto: SLAInputDto
    {
        public int Id { get; set; }
    }
}
