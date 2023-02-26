using ApiApplication.Database.Entities;
using ApiApplication.Mappers;
//using Newtonsoft.Json;
using System.Text.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace ApiApplication.Services
{
    public class ImdbApiService : IImdbApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ImdbApiService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<TitleImdbEntity> GetMovieAsync(string imdbId)
        {
            HttpClient client = _httpClientFactory.CreateClient("imdbApi");

            HttpResponseMessage response = await client.GetAsync($"en/API/Title/k_lcv988tw/{imdbId}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error retrieving movie from IMDB API. Status code: {response.StatusCode}");
            }

            string responseBody = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<TitleImdbEntity>(responseBody);
        }

        public async Task<HttpStatusCode> GetImdbStatusAsync()
        {
            HttpClient client = _httpClientFactory.CreateClient("imdbApi");

            HttpResponseMessage response = await client.GetAsync("");

            return response.StatusCode;
        }
    }
}
