using System.Net.Http;
using System.Threading.Tasks;
using System;
using Newtonsoft.Json;
using System.IO;

namespace ApiApplication.Utils
{
    public class HttpClientWrapper : IHttpClientWrapper
    {
        private readonly IHttpClientFactory _clientFactory;

        public HttpClientWrapper(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<HttpResponseMessage> GetAsyncyHttpResponseMessage<T>(string uri, string requestUri)
        {
            try
            {
                UriBuilder builder = new UriBuilder(uri)
                {
                    Path = requestUri
                };
                var client = _clientFactory.CreateClient(builder.ToString());

                var response = await client.GetAsync(builder.Uri);

                return response;
            }

            catch (Exception e)
            {
                if (e.Source != null)
                    Console.WriteLine("IOException source: {0}", e.Source);
                throw;
            }
        }
        public async Task<T> GetAsync<T>(string uri, string requestUri)
        {
            try
            {

                UriBuilder builder = new UriBuilder(uri)
                {
                    Path = requestUri
                };
                var client = _clientFactory.CreateClient(builder.ToString());

                var response = await client.GetAsync(builder.Uri);

                var json = await response.Content.ReadAsStringAsync();
                var res = JsonConvert.DeserializeObject<T>(json);

                return res;

            }

            catch (Exception e)
            {
                if (e.Source != null)
                    Console.WriteLine("IOException source: {0}", e.Source);
                throw;
            }
        }
    }
}
