using Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dto.CustomDefine
{
    public class RootCauseOutDto
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public int DisplayOrder { get; set; }
        public string Description { get; set; }
        public EnumRootResolutionType Type { get; set; }
        public int FKCompanyId { get; set; }
    }
    public class RootCauseInputDto: RootCauseOutDto
    {
        public EnumCrud Task { get;set; }
    }

}
