using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IssueManagement.BackgroundProcess
{
    public interface ITicketQueueService
    {
        public Task ProcessTicketQueueAsync();
        public Task ProcessNotificationQueueAsync();
    }
    public class TicketQueueService: ITicketQueueService
    {
        public Task ProcessTicketQueueAsync()
        {
            throw new NotImplementedException();
        }

        public Task ProcessNotificationQueueAsync()
        {
            throw new NotImplementedException();
        }
    }
}
