using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ApiApplication.FunctionalTests
{
    public class FunctionalTestStartup : IClassFixture<ApiApplicationWebApplicationFactory>
    {
        protected readonly HttpClient _client;

        protected FunctionalTestStartup()
        {
            var factory = new ApiApplicationWebApplicationFactory();
            _client = factory.CreateClient();
            _client.BaseAddress = new Uri("https://localhost:7629/");
        }

        protected void ReadTokenAuthenticate()
        {
            _client.DefaultRequestHeaders.Add("ApiKey", "MTIzNHxSZWFk");
        }

        protected void WriteTokenAuthenticate()
        {
            _client.DefaultRequestHeaders.Add("ApiKey", "Nzg5NHxXcml0ZQ==");
        }
        protected async Task<HttpResponseMessage> GetAsync(string uri)
        {
            try
            {
                return await _client.GetAsync(uri);
            }
            catch
            {
                return new HttpResponseMessage { StatusCode = HttpStatusCode.NotFound };
            }
        }

        protected async Task<HttpResponseMessage> PostAsync<TEntity>(string uri, TEntity body)
        {
            try
            {
                var json = JsonConvert.SerializeObject(body);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                return await _client.PostAsync(uri, data);
            }
            catch
            {
                return new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest };
            }
        }

        #region Private



        #endregion
    }
}