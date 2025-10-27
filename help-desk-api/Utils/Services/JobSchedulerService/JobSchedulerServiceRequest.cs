
using Utils.Integration.API;

namespace JobSchedulerService
{
    public class JobSchedulerServiceRequest : ApiRequest
    {
        public JobSchedulerServiceRequest(HttpMethod httpMethod, string requestUrl) : base(httpMethod, requestUrl)
        {

        }
    }
}
