using ApiApplication.ImdbApi.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiApplication.ImdbApi
{
    public class ImdbApiClient : IImdbApiClient
    {
        private readonly ImdbSettings _imdbSettings;

        public ImdbApiClient(IOptions<ImdbSettings> imdbSettings)
        {
            if (imdbSettings is null)
            {
                throw new ArgumentNullException(nameof(imdbSettings));
            }

            _imdbSettings = imdbSettings.Value;
        }

        public async Task<HttpStatusCode> GetStatus()
        {
            using HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("https://imdb-api.com/");

            return response.StatusCode;
        }

        public async Task<ImdbMovie> GetMovie(string imdbId)
        {
            using HttpClient client = new HttpClient();

            HttpResponseMessage response = await client.GetAsync($"https://imdb-api.com/en/API/Title/{_imdbSettings.ApiKey}/{imdbId}");

            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();

            var imdbMovie = JsonConvert.DeserializeObject<ImdbMovie>(responseBody);

            return imdbMovie;
        }
    }
}
