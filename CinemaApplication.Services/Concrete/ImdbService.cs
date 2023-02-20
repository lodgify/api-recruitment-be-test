using CinemaApplication.Services.Abstractions;
using CinemaApplication.Services.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CinemaApplication.Services.Concrete
{
    public class ImdbService : IImdbService
    {
        private readonly HttpClient _client;

        public ImdbService(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient("default");
        }

        public async Task<bool> GetImdbStatus()
        {
            var response = await _client.GetAsync("https://www.imdb.com/");
            return response.IsSuccessStatusCode;
        }

        public async Task<ImdbMovie> GetMovieAsync(string imdbId)
        {
            // TODO: Refactor below code
            var response = await _client.SendAsync(new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://imdb8.p.rapidapi.com/title/get-details?tconst={imdbId}"),
                Headers =
                {
                    { "X-RapidAPI-Key", "2cb6f07896mshed6c555fa39c38fp1d618fjsnae88ac9183b8" },
                    { "X-RapidAPI-Host", "imdb8.p.rapidapi.com" },
                }
            });

            response.EnsureSuccessStatusCode();

            var contentStr = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ImdbMovie>(contentStr);
        }
    }
}
