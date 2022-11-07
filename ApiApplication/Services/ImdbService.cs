using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

using ApiApplication.DTO;
using ApiApplication.Models;
using ApiApplication.Models.Configurations;

namespace ApiApplication.Services {
    public class ImdbService : IImdbService {
        private readonly HttpClient _httpClient;
        private readonly IMdbConfig _config;

        public ImdbService(HttpClient httpClient,
                           IMdbConfig config) {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<Result<ImdbMovie>> GetAsync(string id) {
            HttpResponseMessage response = await _httpClient.GetAsync(string.Format(_config.MovieInfoEndPoint, _config.ApiKey, id));
            if (response.IsSuccessStatusCode) {
                ImdbMovie result = await response.Content.ReadFromJsonAsync<ImdbMovie>();
                return new Result<ImdbMovie>(result, ResultCode.Ok);
            }
            return new Result<ImdbMovie>((ResultCode)response.StatusCode);
        }
    }
}
