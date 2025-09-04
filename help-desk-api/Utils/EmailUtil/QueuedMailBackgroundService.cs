using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils.EmailUtil
{
    public class QueuedMailBackgroundService: BackgroundService
    {

        private readonly IMailBackgroundTaskQueue _taskQueue;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<IMailBackgroundTaskQueue> _logger;

        public QueuedMailBackgroundService(IMailBackgroundTaskQueue taskQueue, IServiceProvider serviceProvider, ILogger<IMailBackgroundTaskQueue> logger)
        {
            _taskQueue = taskQueue;
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Background task queue service is starting.");

            while (!stoppingToken.IsCancellationRequested)
            {
                var workItem = await _taskQueue.DequeueAsync(stoppingToken);

                try
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        await workItem(stoppingToken);  // Execute the task
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred executing background task.");
                }
            }

            _logger.LogInformation("Background task queue service is stopping.");
        }
    }
}
