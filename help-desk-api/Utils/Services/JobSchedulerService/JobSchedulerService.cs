
using JobSchedulerService;
using Models.AppSettings;
using Models.Dto.Notification;
using Models.Enum;
using Newtonsoft.Json;
using Serilog;
using Utils.Integration.API;

namespace JobSchedulerService
{
    public interface IJobSchedulerService
    {
        Task<string> Create(JobSchedulerOutputDto jobScheduler);
        Task<bool> Update(JobSchedulerOutputDto jobScheduler);
        Task Delete(string schedulerId, string jobModuleId);
    }
    public class JobSchedulerService : IJobSchedulerService
    {
        string _baseUrl = AppSettings.JobScheduler.BaseUrl;
        IQsClient _qsClient;
        dynamic _response;
        public JobSchedulerService(IQsClient qsClient)
        {
            _qsClient = qsClient;
        }
        public async Task<string> Create(JobSchedulerOutputDto jobScheduler)
        {
            string response = string.Empty;
            try
            {
                JobSchedulerServiceRequest request = new JobSchedulerServiceRequest(HttpMethod.Post, "/api/v1/scheduling");
                request.SetPostContent(jobScheduler);
                _response = _qsClient.Request<object>(request, EnumExternalServiceModules.JobScheduler, true);
            }
            catch (Exception ex)
            {
                Log.Error($"Job scheduler create api request error {ex.Message}");
            }

            try
            {
                response = Convert.ToString(_response.id);
            }
            catch (Exception ex)
            {
                Log.Error($"Job scheduler create DeserializeObject Error: {ex.Message}");
                return null;
            }
            return await Task.FromResult<string>(response);
        }

        public async Task<bool> Update(JobSchedulerOutputDto jobScheduler)
        {
            bool response = false;
            try
            {
                JobSchedulerServiceRequest request = new JobSchedulerServiceRequest(HttpMethod.Put, "/api/v1/scheduling");
                request.SetPostContent(jobScheduler);

                _response = _qsClient.Request<object>(request, EnumExternalServiceModules.JobScheduler);
            }
            catch (Exception ex)
            {
                Log.Error($"Job scheduler update api request error {ex.Message}");
            }

            try
            {
                response = (bool)JsonConvert.DeserializeObject<bool>(Convert.ToString(_response.data.customers));
            }
            catch (Exception ex)
            {
                Log.Error($"Job scheduler create DeserializeObject Error: {ex.Message}");
            }
            return await Task.FromResult<bool>(response);
        }

        public async Task Delete(string schedulerId, string jobModuleId)
        {
            try
            {
                JobSchedulerServiceRequest request = new JobSchedulerServiceRequest(HttpMethod.Delete, "/api/v1/notification?scheduleId=" + schedulerId + "&jobModuleId=" + jobModuleId);

                _response = _qsClient.Request<object>(request, EnumExternalServiceModules.JobScheduler);
            }
            catch (Exception ex)
            {
                Log.Error($"Job scheduler delete api request error {ex.Message}");
            }
        }
    }
}
