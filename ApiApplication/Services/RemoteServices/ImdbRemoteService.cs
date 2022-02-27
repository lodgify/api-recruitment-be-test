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
        public static string test;
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
            //Console.WriteLine(response.Content);
            //return BaseResponseDto.SuccessResult<string>(response.Content);
            if (response.IsSuccessful)
            {
                //return JsonConvert.DeserializeObject<Dictionary<string,object>>(response.Content);
                return JsonConvert.DeserializeObject<MovieDto>(response.Content);
            }
            return null;
            
        }
    }
}
