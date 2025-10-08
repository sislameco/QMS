using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities.Org
{
    public class UpdateTemplateDto
    {
        public int EmailConfigurationId { get; set; }
        public string SubjectTemplate { get; set; }
        public string BodyTemplate { get; set; }
        public string CcList { get; set; }
    }


    public class EmailConfigInputDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string ReplyTo { get; set; }
        public string[] Bcc { get; set; }
        public string[] CcList { get; set; }
    }
}
