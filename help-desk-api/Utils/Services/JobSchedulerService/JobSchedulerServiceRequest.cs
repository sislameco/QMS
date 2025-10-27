
using Utils.Integration.API;

namespace omsService.JobSchedulerService
{
    public class JobSchedulerServiceRequest : ApiRequest
    {
        public JobSchedulerServiceRequest(HttpMethod httpMethod, string requestUrl) : base(httpMethod, requestUrl)
        {

        }
    }
}
