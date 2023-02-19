using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ApiApplication.Constants;
using Newtonsoft.Json;

namespace ApiApplication.Resources
{
    public class ImdbRepository : IImdbRepository
    {
        public ImdbRepository()
        {
        }

        public async Task<ImdbTitleResponse> GetTitle(string imdbId)
        {
            var http = new HttpClient();
            var response = await http.GetAsync($"https://imdb-api.com/en/API/Title/{ImdbConstants.ImdbApiKey}/{imdbId}");
            if (response.StatusCode != System.Net.HttpStatusCode.OK) throw new Exception("Unable to make the http request to IMDB");
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ImdbTitleResponse>(content);
            return result;
        }
    }
}

