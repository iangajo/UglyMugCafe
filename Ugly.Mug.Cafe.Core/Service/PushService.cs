using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using Ugly.Mug.Cafe.Domain.Request;

namespace Ugly.Mug.Cafe.Core.Service
{
    public class PushService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public PushService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public void Push(PushServiceRequest request)
        {
            var requestUri = "api/Message";

            var json = JsonConvert.SerializeObject(request);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var client = _httpClientFactory.CreateClient("push");
            client.PostAsync(requestUri, data);

        }
    }
}
