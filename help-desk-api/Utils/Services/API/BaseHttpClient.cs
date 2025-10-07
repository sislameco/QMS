using Models.AppSettings;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Utilities;

namespace Utils.Integration.API
{
    public abstract class BaseHttpClient
    {
        protected readonly HttpClient _client;
        public BaseHttpClient(HttpClient httpClient)
        {
            httpClient.BaseAddress = new Uri(AppSettings.QsmartAPI.BaseUrl);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("AuthorizationKey", AppSettings.QsmartAPI.AuthorizationKey);
            httpClient.DefaultRequestHeaders.Add("Company", AppSettings.QsmartAPI.Company);
            httpClient.Timeout = TimeSpan.FromMinutes(10);
            _client = httpClient;
        }
    }
}
