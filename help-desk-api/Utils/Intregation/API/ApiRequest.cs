using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Utils.Intregation.API
{
    public class ApiRequest: HttpRequestMessage
    {
        public string _userToken;
        public ApiRequest()
        {

        }
        public ApiRequest(HttpMethod method, string requestUri)
             : base(method, requestUri)
        {

        }
        public virtual void SetPostContent<T>(T data)
        {
            var httpContent = CreateHttpContent(data);
            this.Content = httpContent;
        }
        internal virtual void SetFormDataContent<T>(MultipartFormDataContent data)
        {
            //this.Content.Headers.ContentType= new MediaTypeHeaderValue("application/json");
            this.Content = data;
        }
        private void SerializeJsonIntoStream(object value, Stream stream)
        {
            using (var sw = new StreamWriter(stream, new UTF8Encoding(false), 1024, true))
            using (var jtw = new JsonTextWriter(sw) { Formatting = Formatting.None })
            {
                var js = new JsonSerializer();
                js.Serialize(jtw, value);
                jtw.Flush();
            }
        }
        private HttpContent CreateHttpContent(object content)
        {
            HttpContent httpContent = null;

            if (content != null)
            {
                var ms = new MemoryStream();
                SerializeJsonIntoStream(content, ms);
                ms.Seek(0, SeekOrigin.Begin);
                httpContent = new StreamContent(ms);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            }

            return httpContent;
        }

        internal void SetAuthorizationHeader(string token)
        {
            this._userToken = token;
        }
    }
}
