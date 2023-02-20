using CinemaApplication.Services.Abstractions;
using CinemaApplication.Services.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CinemaApplication.Services.Concrete
{
    public class ImdbService : IImdbService
    {
        private readonly HttpClient _client;
        private readonly ILogger _logger;

        public ImdbService(
            IHttpClientFactory httpClientFactory,
            ILoggerFactory loggerFactory)
        {
            _client = httpClientFactory.CreateClient("default");
            _logger = loggerFactory.CreateLogger<ImdbService>();
        }

        public async Task<ServiceDataResult<bool>> GetImdbStatus()
        {
            try
            {
                var response = await _client.GetAsync("https://www.imdb.com/");
                return ServiceDataResult<bool>.WithData(response.IsSuccessStatusCode);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                return ServiceDataResult<bool>.WithError(ex.Message);
            }
        }

        public async Task<ServiceDataResult<ImdbMovie>> GetMovieAsync(string imdbId)
        {
            try
            {
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
                var movie = JsonConvert.DeserializeObject<ImdbMovie>(contentStr);

                return ServiceDataResult<ImdbMovie>.WithData(movie);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                return ServiceDataResult<ImdbMovie>.WithError(ex.Message);
            }
        }
    }
}
