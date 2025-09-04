using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Utils.EmailUtil
{
    public interface IMailBackgroundTaskQueue
    {
        void QueueBackgroundWorkItem(Func<CancellationToken, Task> task);
        Task<Func<CancellationToken, Task>> DequeueAsync(CancellationToken cancellationToken);
    }

    public class MailBackgroundTaskQueue : IMailBackgroundTaskQueue
    {
        private readonly Channel<Func<CancellationToken, Task>> _queue;

        public MailBackgroundTaskQueue(int capacity)
        {
            // Bounded queue to limit concurrent tasks
            _queue = Channel.CreateBounded<Func<CancellationToken, Task>>(capacity);
        }

        public void QueueBackgroundWorkItem(Func<CancellationToken, Task> workItem)
        {
            if (workItem == null)
                throw new ArgumentNullException(nameof(workItem));

            _queue.Writer.TryWrite(workItem);
        }

        public async Task<Func<CancellationToken, Task>> DequeueAsync(CancellationToken cancellationToken)
        {
            return await _queue.Reader.ReadAsync(cancellationToken);
        }
    }
}
