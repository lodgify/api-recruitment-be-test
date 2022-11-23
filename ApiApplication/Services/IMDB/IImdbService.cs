using ApiApplication.Models.Movies;
using System.Threading.Tasks;

namespace ApiApplication.Services.IMDB
{
    public interface IImdbService
    {
        Task<MovieModel> GetByImdbId(string imdbId);
        Task<bool> CheckApiStatus();
    }
}
