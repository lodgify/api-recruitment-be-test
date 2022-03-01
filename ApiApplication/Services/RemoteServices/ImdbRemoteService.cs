using ApiApplication.Database;
using ApiApplication.Dtos;
using AutoMapper;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Threading.Tasks;

namespace ApiApplication.Services.RemoteServices
{
    public class ImdbRemoteService
    {
        private readonly IOptions<AppSettingsModel> _settings;
        private readonly IMapper _mapper;
        public ImdbRemoteService(IOptions<AppSettingsModel> settings,
            IMapper mapper)
        {
            _settings = settings;
            _mapper = mapper;
        }
        public async Task<MovieDto> GetMovieInformation(string id)
        {
            var imdbToken = _settings.Value.ImdbToken;
            var client = new RestClient($"https://imdb-api.com/en/API/Title/{imdbToken}/{id}");
            var request = new RestRequest();
            var response = await client.ExecuteAsync(request);
            if (response.IsSuccessful)
            {
                //return JsonConvert.DeserializeObject<MovieDto>(response.Content);
                JObject obj = JsonConvert.DeserializeObject<JObject>(response.Content);
                MovieDto movie = new MovieDto
                {
                    Imdb_id = obj["id"].ToString(),
                    Title = obj["title"].ToString(),
                    Stars = obj["stars"].ToString(),
                    Release_date = Convert.ToDateTime(obj["releaseDate"].ToString())
                };
                return movie;
            }
            return null;
            
        }
    }
}
