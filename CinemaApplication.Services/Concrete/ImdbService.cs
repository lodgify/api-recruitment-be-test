using CinemaApplication.DTOs;
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
            _client = httpClientFactory.CreateClient("ImdbAPI");
            _logger = loggerFactory.CreateLogger<ImdbService>();
        }

        public async Task<ServiceDataResult<ImdbStatusDto>> GetImdbStatus()
        {
            try
            {
                var response = await _client.GetAsync("https://www.imdb.com/");
                return ServiceDataResult<ImdbStatusDto>.WithData(new ImdbStatusDto
                {
                    Up = response.IsSuccessStatusCode,
                    LastCall = DateTime.UtcNow
                });
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                return ServiceDataResult<ImdbStatusDto>.WithError(ex.Message);
            }
        }

        public async Task<ServiceDataResult<ImdbApiMovie>> GetMovieAsync(string imdbId)
        {
            try
            {
                var response = await _client.GetAsync($"/title/get-details?tconst={imdbId}");

                response.EnsureSuccessStatusCode();

                var contentStr = await response.Content.ReadAsStringAsync();
                var movie = JsonConvert.DeserializeObject<ImdbApiMovie>(contentStr);

                return ServiceDataResult<ImdbApiMovie>.WithData(movie);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                return ServiceDataResult<ImdbApiMovie>.WithError(ex.Message);
            }
        }
    }
}
