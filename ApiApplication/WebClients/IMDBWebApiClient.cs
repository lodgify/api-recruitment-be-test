using ApiApplication.DTOs.IMDB;
using ApiApplication.Options;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ApiApplication.WebClients
{
    public class IMDBWebApiClient : IIMDBWebApiClient
    {
        private readonly WebApiClientOptions _options;

        public IMDBWebApiClient(IOptions<WebApiClientOptions> options)
        {
            _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
        }

        public async Task<IMDBMovieInfo> GetMovieInfoAsync(string imdbId)
        {
            HttpResponseMessage httpResponseMessage;
            using (var httpClient = new HttpClient())
            {
                var path = $"{_options.IMDBUrl}/{_options.IMDBApiKey}/{imdbId}";
                httpResponseMessage = await httpClient.GetAsync(path);
            }

            httpResponseMessage.EnsureSuccessStatusCode();

            await using var stream = await httpResponseMessage.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<IMDBMovieInfo>(stream);
        }

        public async Task<HttpStatusCode> GetStatus()
        {
            HttpResponseMessage httpResponseMessage;
            using (var httpClient = new HttpClient())
            {
                var path = $"{_options.IMDBUrl}/{_options.IMDBApiKey}";
                httpResponseMessage = await httpClient.GetAsync(path);
            }

            return httpResponseMessage.StatusCode;
        }
    }
}
