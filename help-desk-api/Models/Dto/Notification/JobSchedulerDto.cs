using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dto.Notification
{
    public class JobSchedulerOutputDto
    {
        public string Id { get; set; }
        public DateTime ScheduledDateTime { get; set; }
        public string SyncUrl { get; set; }
        public string Module { get; set; }
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }

    }
}
