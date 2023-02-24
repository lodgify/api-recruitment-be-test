using ApiApplication.Database.Entities;
using ApiApplication.Mappers;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiApplication.Services
{
    public class ImdbService : IImdbService
    {
        public async Task<MovieEntity> GetMovieAsync(string imdbId)
        {
            HttpClient client = new HttpClient();

            HttpResponseMessage response = await client.GetAsync($"https://imdb-api.com/en/API/Title/k_lcv988tw/{imdbId}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error retrieving movie from IMDB API. Status code: {response.StatusCode}");
//                return BadRequest($"Error retrieving movie from IMDB API. Status code: {response.StatusCode}");
            }

            string responseBody = await response.Content.ReadAsStringAsync();
            TitleImdbEntity titleImdbEntity = JsonConvert.DeserializeObject<TitleImdbEntity>(responseBody);

            return MovieMapper.MapToEntity(titleImdbEntity);
        }
    }
}
