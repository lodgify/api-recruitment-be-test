using ApiApplication.Models;
using ApiApplication.Services.Implementation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace ApiApplication.Services
{
    public class ServiceImdbApi : IServiceImdbApi
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private const string _apiClientName = "ImdbApi";
        private readonly string _apiKey;
        private readonly ILogger<ServiceImdbApi> _logger;

        public ServiceImdbApi(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<ServiceImdbApi> logger )
        {
            _httpClientFactory = httpClientFactory;
            _apiKey = configuration.GetSection("ImdbService:ApiKey").Value.Trim();
            _logger = logger;
        }

        public async Task<bool> GetApiStatus()
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient(_apiClientName);
                var httpResponseMessage = await httpClient.GetAsync($"Usage/{_apiKey}");

                return httpResponseMessage.StatusCode switch
                {
                    HttpStatusCode.OK => true,
                    HttpStatusCode.Forbidden or HttpStatusCode.NotFound => false,
                    _ => false
                };
            }
            catch (Exception ex)
            {                
                _logger.LogError(ex, "An error occurred while getting the API status.");
                return false;
            }
        }
        public async Task<Movie> GetMovieDetails(string imdbId)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient(_apiClientName);
                var httpResponseMessage = await httpClient.GetAsync($"Title/{_apiKey}/{imdbId}");

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var contentStream = await httpResponseMessage.Content.ReadAsStringAsync();
                    var imdbMovie = JsonConvert.DeserializeObject<Movie>(contentStream);
                    imdbMovie.ImdbId = imdbId;

                    return imdbMovie;
                }

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting the API MovieDetails.");
                throw;
            }
        }
    }
}
