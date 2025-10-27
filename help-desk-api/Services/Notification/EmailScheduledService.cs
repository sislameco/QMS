using Models.Dto.Notification;
using omsService.JobSchedulerService;

namespace Services.Notification
{
    public interface IEmailScheduledService
    {
        Task<bool> EmailScheduled();
    }
    public class EmailScheduledService : IEmailScheduledService
    {
        private readonly IJobSchedulerService _jobSchedulerService;
        public EmailScheduledService(IJobSchedulerService jobSchedulerService)
        {
            _jobSchedulerService = jobSchedulerService;
        }
        public async Task<bool> EmailScheduled()
        {
            string syncUrl = ""; // Define the sync URL for email scheduling

            var job = new JobSchedulerOutputDto
            {
                Id = "",
                Module = "SupplierManagement",
                ScheduledDateTime = DateTime.UtcNow, // Schedule to run immediately
                SyncUrl = syncUrl,
                AccessKey = string.Empty,
                SecretKey = string.Empty
            };

            var result = await _jobSchedulerService.Create(job);
            return true;
        }

    }
}
