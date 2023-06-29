using ApiApplication.Models;
using System.Threading.Tasks;

namespace ApiApplication.Services.Implementation
{
    public interface IServiceImdbApi
    {
        Task<Movie> GetMovieDetails(string imdbId);
        Task<bool> GetApiStatus();
    }
}