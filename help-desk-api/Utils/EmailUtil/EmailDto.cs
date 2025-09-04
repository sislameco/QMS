using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils.EmailUtil
{
    public class EmailSendDto
    {
        public List<string> ToEmailAddresses { get; set; } = new List<string>();
        public List<string> CcEmailAddresses { get; set; } = new List<string>();
        public required string Mailbody { get; set; }
        public required string MailSubject { get; set; }
        public List<string> BccEmailAddresses { get; set; } = new List<string>();
    }
}
