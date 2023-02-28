using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lodgify.Cinema.Infrastructure.Data.Http
{
    public abstract class BaseHttpRepository
    {
        #region [prop]

        private readonly HttpClient _httpClient;
        private readonly ILogger _logger;

        #endregion [prop]

        #region [ctor]

        protected BaseHttpRepository(HttpClient httpClient, ILogger logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        #endregion [ctor]

        #region [HttpMessage]

        private HttpRequestMessage GetMessage(string url, object data) => MakeMessage(HttpMethod.Get, url, data);
        private HttpRequestMessage PostMessage(string url, object data) => MakeMessage(HttpMethod.Post, url, data);
        private HttpRequestMessage PutMessage(string url, object data) => MakeMessage(HttpMethod.Put, url, data);
        private HttpRequestMessage DeleteMessage(string url, object data) => MakeMessage(HttpMethod.Delete, url, data);
        private HttpRequestMessage PatchMessage(string url, object data) => MakeMessage(HttpMethod.Patch, url, data);

        private HttpRequestMessage MakeMessage(HttpMethod method, string url, object data)
        {
            HttpRequestMessage message = new HttpRequestMessage();

            if (data != null)
            {
                if (method == HttpMethod.Get)
                {

                    url = MakeRequestGetParameters(url, data);
                }
                else
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                }
            }

            message.Method = method;
            string address = $"{_httpClient.BaseAddress}{GetRoute(url)}";

            message.RequestUri = new Uri(address);

            return message;
        }

        private string MakeRequestGetParameters(string url, object data)
        {
            url = $"{url}?";

            StringBuilder sbParameters = new StringBuilder();
            string param = string.Empty;

            var type = data.GetType();

            if (type == typeof(string))
            {
                sbParameters.Append(data.ToString());
            }
            else
            {
                var properties = data.GetType().GetProperties();
                for (int i = 0; i < properties.Length; i++)
                {
                    var prop = properties[i];
                    param = $"{prop.Name}={prop.GetValue(data)}";
                    sbParameters.Append(i == 0 ? param : $"&{param}");
                }
            }
            string completeAddress = $"{url}{sbParameters}";
            return completeAddress;
        }

        public async Task<T> GetResult<T>(HttpResponseMessage message, CancellationToken cancellationToken)
        {
            if (message.Content is object && message.Content.Headers.ContentType.MediaType == "application/json")
            {
                var contentStream = await message.Content.ReadAsStreamAsync();

                using var streamReader = new StreamReader(contentStream);
                using var jsonReader = new JsonTextReader(streamReader);

                JsonSerializer serializer = new JsonSerializer();

                try
                {
                    var obj = serializer.Deserialize<T>(jsonReader);
                    return obj;
                }
                catch (Exception ex)
                {
                    _logger.Log(LogLevel.Error, $"It's not possible to desserialize returned JSON:{ex}");
                    return default(T);
                }
            }
            return default(T);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="routeAction">Action</param>
        /// <param name="page">Current Page</param>
        /// <param name="pageSize">Itens per page</param>
        /// <returns></returns>
        protected virtual string GetRoute(string routeAction)
        {
            return _httpClient.BaseAddress.AbsoluteUri.EndsWith("/")
                ? $"{routeAction}"
                : $"/{routeAction}";
        }

        #endregion [HttpMessage]

        #region [Methods]

        protected async Task<T> GetAsync<T>(string action, CancellationToken cancellationToken)
        {
            return await GetAsync<T>(action, null, cancellationToken);
        }

        protected async Task<T> GetAsync<T>(string action, object data, CancellationToken cancellationToken)
        {
            var request = GetMessage(action, data);
            var response = await _httpClient.SendAsync(request, cancellationToken);
            return await GetResult<T>(response, cancellationToken);
        }

        #endregion [Methods]
    }
}
