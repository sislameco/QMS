using Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Models.Dto.Org
{
    public class SLAInputDto
    {
        public EnumQMSType Type { get; set; }
        public EnumPriority Priority { get; set; }
        public int FKCompanyId { get; set; }
        public EnumUnit Unit { get; set; }
        public int ResponseTime { get; set; }
        public int ResolutionTime { get; set; }
        public int EscalationTime { get; set; }
    }
    public class SLAOutputDto: SLAInputDto
    {
        public int Id { get; set; }
        [JsonIgnore]
        public EnumRStatus Status { get; set; }
    }
}
