using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ApiApplication.Helpers
{
    public class ImdbApiHelper: IImdbHelper
    {
        private string _apiKey { get; set; }

        private string _language { get; set; }

        private string _baseUrl 
        { 
            get 
            { 
                return $"https://imdb-api.com/{this._language}/API/[API_NAME]/{this._apiKey}";
            } 
        }

        public ImdbApiHelper(string apiKey, string language)
        {
            this._apiKey = apiKey;
            this._language = language;
        }

        public ImdbApiHelper(string apiKey)
        {
            this._apiKey = apiKey;
            this._language = "en";
        }

        public async Task<Dictionary<string, object>> GetMovieInformationByIdAsync(string imdbId)
        {
            return await _InvokeApi("Title", imdbId);
        }

        public async Task<Dictionary<string, object>> GetMoviePostersByIdAsync(string imdbId)
        {
            return await _InvokeApi("Posters", imdbId);
        }

        private async Task<Dictionary<string, object>> _InvokeApi(string apiName, string imdbId)
        {
            var baseAddress = new Uri(_baseUrl.Replace("[API_NAME]", apiName)) + $"/{imdbId}";

            var handler = new HttpClientHandler();
            handler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (var client = new HttpClient(handler))
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                using (HttpResponseMessage response = await client.GetAsync(baseAddress, HttpCompletionOption.ResponseHeadersRead))
                {
                    var contentStream = await response.Content.ReadAsStreamAsync();
                    return await JsonSerializer.DeserializeAsync<Dictionary<string, object>>(contentStream, new System.Text.Json.JsonSerializerOptions { IgnoreNullValues = true, PropertyNameCaseInsensitive = true });
                }
            }
        }
    }
}
