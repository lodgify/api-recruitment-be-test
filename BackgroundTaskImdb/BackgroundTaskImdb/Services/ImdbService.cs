using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace BackgroundTaskImdb.Services
{
    public class ImdbService : IImdbService
    {
        public async Task<HttpStatusCode> GetImdbStatusAsync()
        {
            HttpClient client = new HttpClient();

            HttpResponseMessage response = await client.GetAsync("https://imdb-api.com/");
//            HttpResponseMessage response = await client.GetAsync($"https://imdb-api.com/en/API/Title/k_lcv988tw/{imdbId}");

            return response.StatusCode;
        }
    }
}
