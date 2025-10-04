using Models.Dto.Intregation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Intregation.API;

namespace Utils.Intregation.Request
{
    public class UserServiceRequest : ApiRequest
    {
        public UserServiceRequest(HttpMethod httpMethod, string requestUrl) : base(httpMethod, requestUrl)
        {

        }
    }
    public class DepartmentServiceRequest : ApiRequest
    {
        public DepartmentServiceRequest(HttpMethod httpMethod, string requestUrl) : base(httpMethod, requestUrl)
        {

        }
    }
}
