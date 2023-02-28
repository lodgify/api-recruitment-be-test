using Lodgify.Cinema.AcceptanceTest.TestServers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Lodgify.Cinema.AcceptanceTest.Core
{
    public static class HttpExtensions
    {
        public static HttpRequestMessage Post(this HttpClient client, string url, object data, string apiPrefix = "api/") => GetMessage(HttpMethod.Post, url, data, apiPrefix);
        public static HttpRequestMessage Get(this HttpClient client, string url, object data, string apiPrefix = "api/") => GetMessage(HttpMethod.Get, url, data, apiPrefix);
        public static HttpRequestMessage Put(this HttpClient client, string url, object data, string apiPrefix = "api/") => GetMessage(HttpMethod.Put, url, data, apiPrefix);
        public static HttpRequestMessage Delete(this HttpClient client, string url, object data, string apiPrefix = "api/") => GetMessage(HttpMethod.Delete, url, data, apiPrefix);

        private static HttpRequestMessage GetMessage(HttpMethod method, string url, object data, string apiPrefix = "api/")
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
            string address = $"{MoviesApiTestServer.Server.BaseAddress}{apiPrefix}{url}";
            message.RequestUri = new Uri(address);
            return message;
        }

        private static string MakeRequestGetParameters(string url, object data)
        {
            var properties = data.GetType().GetProperties();

            url = !url.EndsWith("/")
                ? $"{url}/?"
                : $"{url}?";

            StringBuilder sbParameters = new StringBuilder();
            string param = string.Empty;

            for (int i = 0; i < properties.Length; i++)
            {
                var prop = properties[i];
                param = $"{prop.Name}={prop.GetValue(data)}";
                sbParameters.Append(i == 0 ? param : $"&{param}");
            }
            string completeAddress = $"{url}{sbParameters}";
            return completeAddress;
        }

        public static async Task<T> GetResult<T>(this HttpResponseMessage message)
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
                catch (JsonReaderException)
                {
                    Console.WriteLine("Invalid JSON.");
                    throw;
                }
            }

            return default(T);
        }

        public static async Task<IEnumerable<string>> GetResultMessages(this HttpResponseMessage message)
        {
            return (await GetResult<string[]>(message)).AsEnumerable();
        }

    }
}