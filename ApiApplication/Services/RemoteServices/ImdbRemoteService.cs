using ApiApplication.Database;
using ApiApplication.Dtos;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiApplication.Services.RemoteServices
{
    public class ImdbRemoteService
    {
        private readonly IOptions<AppSettingsModel> _settings;
        public ImdbRemoteService(IOptions<AppSettingsModel> settings)
        {
            _settings = settings;
        }
        public async Task<MovieDto> GetMovieInformation(string id)
        {
            var imdbToken = _settings.Value.ImdbToken;
            var client = new RestClient($"https://imdb-api.com/en/API/Title/{imdbToken}/{id}");
            var request = new RestRequest();
            var response = await client.ExecuteAsync(request);
            if (response.IsSuccessful)
            {
                return JsonConvert.DeserializeObject<MovieDto>(response.Content);
            }
            return null;
            
        }
    }
}
