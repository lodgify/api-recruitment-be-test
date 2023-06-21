using ApiApplication.ImdbApi.Models;
using System.Net;
using System.Threading.Tasks;

namespace ApiApplication.ImdbApi
{
    public interface IImdbApiClient
    {
        Task<HttpStatusCode> GetStatus();

        Task<ImdbMovie> GetMovie(string imdbId);
    }
}
