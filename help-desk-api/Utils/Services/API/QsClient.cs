using Models.AppSettings;
using Models.Enum;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using Utils.Exceptions;

namespace Utils.Integration.API
{
    public interface IQsClient
    {
        public T Request<T>(ApiRequest request, EnumExternalServiceModules module, bool isCustomClient = false);
        public Task<T> RequestAsync<T>(ApiRequest request, EnumExternalServiceModules module, bool isCustomClient = false);
    }
    public class QsClient : BaseHttpClient, IQsClient
    {
        public QsClient(HttpClient httpClient) : base(httpClient)
        {

        }
        T IQsClient.Request<T>(ApiRequest request, EnumExternalServiceModules module, bool isCustomClient)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.Timeout = TimeSpan.FromMinutes(10);

            HttpResponseMessage httpResponseMessage = MakeRequest(request, httpClient, module);
            return JsonConvert.DeserializeObject<T>(httpResponseMessage.Content.ReadAsStringAsync().Result);
        }

        public async Task<T> RequestAsync<T>(ApiRequest request, EnumExternalServiceModules module, bool isCustomClient = false)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.Timeout = TimeSpan.FromMinutes(10);

            HttpResponseMessage httpResponseMessage = MakeRequest(request, httpClient, module);
            return JsonConvert.DeserializeObject<T>(await httpResponseMessage.Content.ReadAsStringAsync());
        }

        private HttpResponseMessage MakeRequest(ApiRequest apiRequest, HttpClient httpClient, EnumExternalServiceModules module)
        {
            switch (module)
            {
                case EnumExternalServiceModules.QsmartAPI:
                    httpClient.BaseAddress = new Uri(AppSettings.QsmartAPI.BaseUrl);
                    httpClient.DefaultRequestHeaders.Add("AuthorizationKey", AppSettings.QsmartAPI.AuthorizationKey);
                    httpClient.DefaultRequestHeaders.Add("Company", AppSettings.QsmartAPI.Company);
                    break;
                case EnumExternalServiceModules.HRAPI:
                    httpClient.BaseAddress = new Uri(AppSettings.HRAPI.BaseUrl);
                    httpClient.DefaultRequestHeaders.Add("AccessKey", "LVM9Vsj2FD");
                    httpClient.DefaultRequestHeaders.Add("SecretKey", "WoxE9NVkeA");
                    break;
                default:
                    break;
            }

            try
            {
                //httpClient.Timeout = TimeSpan.FromMinutes(10);
                HttpResponseMessage response = httpClient.SendAsync(apiRequest, HttpCompletionOption.ResponseContentRead).Result;
                if (!response.IsSuccessStatusCode)
                {
                    var content = response.Content.ReadAsStringAsync().Result;
                    var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(content);
                    switch (errorResponse.StatusCode)
                    {
                        case (int)HttpStatusCode.Unauthorized:
                            throw new UnAuthorizedException(errorResponse.Message);
                        case (int)HttpStatusCode.BadRequest:
                            throw new BadRequestException(errorResponse.Message);
                        default:
                            throw new Exception(errorResponse.Message);
                    }
                }
                response.EnsureSuccessStatusCode();
                return response;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
